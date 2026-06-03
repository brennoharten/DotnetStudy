using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UsuarioApi.Models
{
    public class LandingPage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        // Reference to the Usuario stored in Postgres (IdentityUser.Id)
        public string UsuarioId { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string HeroImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<string> Features { get; set; } = new();

        public List<Service> Services { get; set; } = new();

        public List<PricingPlan> PricingPlans { get; set; } = new();

        public ContactInfo Contact { get; set; } = new ContactInfo();

        public List<SocialLink> SocialLinks { get; set; } = new();

        public List<Testimonial> Testimonials { get; set; } = new();

        public Dictionary<string, string> OpeningHours { get; set; } = new();

        public List<string> GalleryUrls { get; set; } = new();

        public CallToAction CallToAction { get; set; } = new CallToAction();

        public SeoMeta Seo { get; set; } = new SeoMeta();

        public Location Location { get; set; } = new Location();

        public List<Faq> Faqs { get; set; } = new();

        public List<string> Tags { get; set; } = new();

        public bool IsPublished { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Service
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
    }

    public class PricingPlan
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Interval { get; set; } = "monthly";
        public List<string> Benefits { get; set; } = new();
    }

    public class ContactInfo
    {
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class SocialLink
    {
        public string Platform { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    public class Testimonial
    {
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class CallToAction
    {
        public string Text { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonUrl { get; set; } = string.Empty;
    }

    public class SeoMeta
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Keywords { get; set; } = new();
    }

    public class Location
    {
        public string Address { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string GoogleMapsEmbed { get; set; } = string.Empty;
    }

    public class Faq
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}
