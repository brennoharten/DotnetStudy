using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using UsuarioApi.Data;
using UsuarioApi.Data.Dtos.LandingPage;
using UsuarioApi.Infrastructure.Mongo;
using UsuarioApi.Models;
using MongoLandingPage = UsuarioApi.Infrastructure.Mongo.Models.LandingPage;
using MongoService = UsuarioApi.Infrastructure.Mongo.Models.Service;
using MongoPricingPlan = UsuarioApi.Infrastructure.Mongo.Models.PricingPlan;
using MongoContactInfo = UsuarioApi.Infrastructure.Mongo.Models.ContactInfo;
using MongoSocialLink = UsuarioApi.Infrastructure.Mongo.Models.SocialLink;
using MongoTestimonial = UsuarioApi.Infrastructure.Mongo.Models.Testimonial;
using MongoCallToAction = UsuarioApi.Infrastructure.Mongo.Models.CallToAction;
using MongoSeoMeta = UsuarioApi.Infrastructure.Mongo.Models.SeoMeta;
using MongoLocation = UsuarioApi.Infrastructure.Mongo.Models.Location;
using MongoFaq = UsuarioApi.Infrastructure.Mongo.Models.Faq;

namespace UsuarioApi.Services
{
    public interface ILandingPageService
    {
        Task<ReadLandingPageDto> CreateAsync(string usuarioId, CreateLandingPageDto dto);
        Task<ReadLandingPageDto> GetByUsuarioIdAsync(string usuarioId);
        Task<ReadLandingPageDto> GetByIdAsync(string landingPageId);
        Task<ReadLandingPageDto> UpdateAsync(string usuarioId, string landingPageId, CreateLandingPageDto dto);
        Task<bool> DeleteAsync(string usuarioId, string landingPageId);
    }

    public class LandingPageService : ILandingPageService
    {
        private readonly MongoDbContext _mongoContext;
        private readonly UsuarioDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public LandingPageService(
            MongoDbContext mongoContext,
            UsuarioDbContext dbContext,
            IMapper mapper,
            UserManager<Usuario> userManager)
        {
            _mongoContext = mongoContext;
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ReadLandingPageDto> CreateAsync(string usuarioId, CreateLandingPageDto dto)
        {
            // Verify usuario exists
            var usuario = await _userManager.FindByIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            // Check if usuario already has a landing page
            if (!string.IsNullOrEmpty(usuario.LandingPageId))
                throw new Exception("Este usuário já possui uma landing page. Use atualizar ao invés de criar.");

            var landingPage = _mapper.Map<MongoLandingPage>(dto);
            landingPage.UsuarioId = usuarioId;
            landingPage.Slug = CreateSlug(landingPage.Title);
            landingPage.CreatedAt = DateTime.UtcNow;
            landingPage.UpdatedAt = DateTime.UtcNow;

            await _mongoContext.LandingPages.InsertOneAsync(landingPage);

            // Update usuario with landing page reference
            usuario.LandingPageId = landingPage.Id;
            await _userManager.UpdateAsync(usuario);

            return _mapper.Map<ReadLandingPageDto>(landingPage);
        }

        public async Task<ReadLandingPageDto> GetByUsuarioIdAsync(string usuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            if (string.IsNullOrEmpty(usuario.LandingPageId))
                throw new Exception("Este usuário não possui uma landing page.");

            return await GetByIdAsync(usuario.LandingPageId);
        }

        public async Task<ReadLandingPageDto> GetByIdAsync(string landingPageId)
        {
            if (string.IsNullOrEmpty(landingPageId))
                throw new ArgumentException("Landing page ID não pode ser vazio.");

            var filter = Builders<MongoLandingPage>.Filter.Eq(lp => lp.Id, landingPageId);
            var landingPage = await _mongoContext.LandingPages.Find(filter).FirstOrDefaultAsync();

            if (landingPage == null)
                throw new Exception("Landing page não encontrada.");

            return _mapper.Map<ReadLandingPageDto>(landingPage);
        }

        public async Task<ReadLandingPageDto> UpdateAsync(string usuarioId, string landingPageId, CreateLandingPageDto dto)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            if (usuario.LandingPageId != landingPageId)
                throw new Exception("Essa landing page não pertence a este usuário.");

            var filter = Builders<MongoLandingPage>.Filter.Eq(lp => lp.Id, landingPageId);
            var update = Builders<MongoLandingPage>.Update
                .Set(lp => lp.Title, dto.Title)
                .Set(lp => lp.Subtitle, dto.Subtitle)
                .Set(lp => lp.HeroImageUrl, dto.HeroImageUrl)
                .Set(lp => lp.Description, dto.Description)
                .Set(lp => lp.Slug, CreateSlug(dto.Title))
                .Set(lp => lp.Features, dto.Features)
                .Set(lp => lp.Services, _mapper.Map<List<MongoService>>(dto.Services))
                .Set(lp => lp.PricingPlans, _mapper.Map<List<MongoPricingPlan>>(dto.PricingPlans))
                .Set(lp => lp.Contact, _mapper.Map<MongoContactInfo>(dto.Contact))
                .Set(lp => lp.SocialLinks, _mapper.Map<List<MongoSocialLink>>(dto.SocialLinks))
                .Set(lp => lp.Testimonials, _mapper.Map<List<MongoTestimonial>>(dto.Testimonials))
                .Set(lp => lp.OpeningHours, dto.OpeningHours)
                .Set(lp => lp.GalleryUrls, dto.GalleryUrls)
                .Set(lp => lp.CallToAction, _mapper.Map<MongoCallToAction>(dto.CallToAction))
                .Set(lp => lp.Seo, _mapper.Map<MongoSeoMeta>(dto.Seo))
                .Set(lp => lp.Location, _mapper.Map<MongoLocation>(dto.Location))
                .Set(lp => lp.Faqs, _mapper.Map<List<MongoFaq>>(dto.Faqs))
                .Set(lp => lp.Tags, dto.Tags)
                .Set(lp => lp.IsPublished, dto.IsPublished)
                .Set(lp => lp.UpdatedAt, DateTime.UtcNow);

            var options = new FindOneAndUpdateOptions<MongoLandingPage> { ReturnDocument = ReturnDocument.After };
            var updatedLandingPage = await _mongoContext.LandingPages.FindOneAndUpdateAsync(filter, update, options);

            if (updatedLandingPage == null)
                throw new Exception("Erro ao atualizar landing page.");

            return _mapper.Map<ReadLandingPageDto>(updatedLandingPage);
        }

        public async Task<bool> DeleteAsync(string usuarioId, string landingPageId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            if (usuario.LandingPageId != landingPageId)
                throw new Exception("Essa landing page não pertence a este usuário.");

            var filter = Builders<MongoLandingPage>.Filter.Eq(lp => lp.Id, landingPageId);
            var result = await _mongoContext.LandingPages.DeleteOneAsync(filter);

            if (result.DeletedCount > 0)
            {
                usuario.LandingPageId = null;
                await _userManager.UpdateAsync(usuario);
                return true;
            }

            return false;
        }

        private static string CreateSlug(string title)
        {
            var normalized = title.Trim().ToLowerInvariant();
            var slug = normalized
                .Replace("á", "a")
                .Replace("à", "a")
                .Replace("ã", "a")
                .Replace("â", "a")
                .Replace("é", "e")
                .Replace("ê", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ô", "o")
                .Replace("õ", "o")
                .Replace("ú", "u")
                .Replace("ç", "c");

            slug = string.Join("-", slug.Split(new[] { ' ', '/', '\\', '.', ',', ';', ':', '!', '?', '(', ')', '[', ']' }, StringSplitOptions.RemoveEmptyEntries));
            return slug;
        }
    }
}
