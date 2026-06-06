using AutoMapper;
using UsuarioApi.Data.Dtos.LandingPage;
using UsuarioApi.Data.Dtos.Public;
using UsuarioApi.Infrastructure.Mongo.Models;

namespace UsuarioApi.Profiles
{
    public class LandingPageProfile : Profile
    {
        public LandingPageProfile()
        {
            // LandingPage mappings
            CreateMap<CreateLandingPageDto, LandingPage>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

            CreateMap<LandingPage, ReadLandingPageDto>();

            // Service mappings
            CreateMap<ServiceDto, Service>();
            CreateMap<Service, ServiceDto>();

            // PricingPlan mappings
            CreateMap<PricingPlanDto, PricingPlan>();
            CreateMap<PricingPlan, PricingPlanDto>();

            // ContactInfo mappings
            CreateMap<ContactInfoDto, ContactInfo>();
            CreateMap<ContactInfo, ContactInfoDto>();

            // SocialLink mappings
            CreateMap<SocialLinkDto, SocialLink>();
            CreateMap<SocialLink, SocialLinkDto>();

            // Testimonial mappings
            CreateMap<TestimonialDto, Testimonial>();
            CreateMap<Testimonial, TestimonialDto>();

            // CallToAction mappings
            CreateMap<CallToActionDto, CallToAction>();
            CreateMap<CallToAction, CallToActionDto>();

            // SeoMeta mappings
            CreateMap<SeoMetaDto, SeoMeta>();
            CreateMap<SeoMeta, SeoMetaDto>();

            // Location mappings
            CreateMap<LocationDto, Location>();
            CreateMap<Location, LocationDto>();

            // Faq mappings
            CreateMap<FaqDto, Faq>();
            CreateMap<Faq, FaqDto>();

            // Public mappings
            CreateMap<Service, PublicServiceDto>();
            CreateMap<PricingPlan, PublicPricingPlanDto>();
            CreateMap<ContactInfo, PublicContactDto>();
            CreateMap<SocialLink, PublicSocialLinkDto>();
            CreateMap<Testimonial, PublicTestimonialDto>();
            CreateMap<CallToAction, PublicCallToActionDto>();
            CreateMap<SeoMeta, PublicSeoDto>();
            CreateMap<Location, PublicLocationDto>();
            CreateMap<Faq, PublicFaqDto>();
        }
    }
}
