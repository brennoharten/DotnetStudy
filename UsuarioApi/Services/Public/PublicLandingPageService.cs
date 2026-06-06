using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UsuarioApi.Data.Dtos.Public;
using UsuarioApi.Infrastructure.Mongo.Models;
using UsuarioApi.Repositories;

namespace UsuarioApi.Services.Public;

public class PublicLandingPageService : IPublicLandingPageService
{
    private readonly IPublicLandingPageRepository _repository;
    private readonly IMapper _mapper;

    public PublicLandingPageService(IPublicLandingPageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PublicLandingPageDto> GetBySlugAsync(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Slug inválido.");

        var landingPage = await _repository.GetPublishedBySlugAsync(slug);
        if (landingPage == null)
            throw new Exception("Landing page não encontrada.");

        return new PublicLandingPageDto
        {
            Slug = landingPage.Slug,
            Title = landingPage.Title,
            Subtitle = landingPage.Subtitle,
            HeroImageUrl = landingPage.HeroImageUrl,
            Description = landingPage.Description,
            Features = landingPage.Features,
            Services = _mapper.Map<System.Collections.Generic.List<PublicServiceDto>>(landingPage.Services),
            PricingPlans = _mapper.Map<System.Collections.Generic.List<PublicPricingPlanDto>>(landingPage.PricingPlans),
            Contact = _mapper.Map<PublicContactDto>(landingPage.Contact),
            SocialLinks = _mapper.Map<System.Collections.Generic.List<PublicSocialLinkDto>>(landingPage.SocialLinks),
            Testimonials = _mapper.Map<System.Collections.Generic.List<PublicTestimonialDto>>(landingPage.Testimonials),
            OpeningHours = landingPage.OpeningHours,
            GalleryUrls = landingPage.GalleryUrls,
            CallToAction = _mapper.Map<PublicCallToActionDto>(landingPage.CallToAction),
            Seo = _mapper.Map<PublicSeoDto>(landingPage.Seo),
            Location = _mapper.Map<PublicLocationDto>(landingPage.Location),
            Faqs = _mapper.Map<System.Collections.Generic.List<PublicFaqDto>>(landingPage.Faqs),
            Tags = landingPage.Tags,
            IsPublished = landingPage.IsPublished
        };
    }
}
