using System;
using System.Collections.Generic;

namespace UsuarioApi.Data.Dtos.LandingPage
{
    public class ReadLandingPageDto
    {
        public string Id { get; set; } = null!;
        public string UsuarioId { get; set; } = null!;
        public string Slug { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string HeroImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<string> Features { get; set; } = new();

        public List<ServiceDto> Services { get; set; } = new();

        public List<PricingPlanDto> PricingPlans { get; set; } = new();

        public ContactInfoDto Contact { get; set; } = new();

        public List<SocialLinkDto> SocialLinks { get; set; } = new();

        public List<TestimonialDto> Testimonials { get; set; } = new();

        public Dictionary<string, string> OpeningHours { get; set; } = new();

        public List<string> GalleryUrls { get; set; } = new();

        public CallToActionDto CallToAction { get; set; } = new();

        public SeoMetaDto Seo { get; set; } = new();

        public LocationDto Location { get; set; } = new();

        public List<FaqDto> Faqs { get; set; } = new();

        public List<string> Tags { get; set; } = new();

        public bool IsPublished { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
