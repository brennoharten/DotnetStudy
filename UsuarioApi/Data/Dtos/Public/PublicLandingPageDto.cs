using System;
using System.Collections.Generic;

namespace UsuarioApi.Data.Dtos.Public;

public class PublicLandingPageDto
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string HeroImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Features { get; set; } = new();
    public List<PublicServiceDto> Services { get; set; } = new();
    public List<PublicPricingPlanDto> PricingPlans { get; set; } = new();
    public PublicContactDto Contact { get; set; } = new();
    public List<PublicSocialLinkDto> SocialLinks { get; set; } = new();
    public List<PublicTestimonialDto> Testimonials { get; set; } = new();
    public Dictionary<string, string> OpeningHours { get; set; } = new();
    public List<string> GalleryUrls { get; set; } = new();
    public PublicCallToActionDto CallToAction { get; set; } = new();
    public PublicSeoDto Seo { get; set; } = new();
    public PublicLocationDto Location { get; set; } = new();
    public List<PublicFaqDto> Faqs { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public bool IsPublished { get; set; }
}

public class PublicServiceDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
}

public class PublicPricingPlanDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Interval { get; set; } = string.Empty;
    public List<string> Benefits { get; set; } = new();
}

public class PublicContactDto
{
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

public class PublicSocialLinkDto
{
    public string Platform { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class PublicTestimonialDto
{
    public string Author { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class PublicCallToActionDto
{
    public string Text { get; set; } = string.Empty;
    public string ButtonText { get; set; } = string.Empty;
    public string ButtonUrl { get; set; } = string.Empty;
}

public class PublicSeoDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Keywords { get; set; } = new();
}

public class PublicLocationDto
{
    public string Address { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string GoogleMapsEmbed { get; set; } = string.Empty;
}

public class PublicFaqDto
{
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}
