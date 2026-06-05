using System;
using System.Collections.Generic;

namespace UsuarioApi.Data.Dtos.LandingPage
{
    public class CreateLandingPageDto
    {
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
    }

    public class ServiceDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
    }

    public class PricingPlanDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Interval { get; set; } = "monthly";
        public List<string> Benefits { get; set; } = new();
    }

    public class ContactInfoDto
    {
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class SocialLinkDto
    {
        public string Platform { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    public class TestimonialDto
    {
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class CallToActionDto
    {
        public string Text { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonUrl { get; set; } = string.Empty;
    }

    public class SeoMetaDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Keywords { get; set; } = new();
    }

    public class LocationDto
    {
        public string Address { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string GoogleMapsEmbed { get; set; } = string.Empty;
    }

    public class FaqDto
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
