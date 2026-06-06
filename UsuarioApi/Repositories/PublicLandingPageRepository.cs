using System.Threading.Tasks;
using MongoDB.Driver;
using UsuarioApi.Infrastructure.Mongo;
using UsuarioApi.Infrastructure.Mongo.Models;

namespace UsuarioApi.Repositories;

public class PublicLandingPageRepository : IPublicLandingPageRepository
{
    private readonly MongoDbContext _mongoDbContext;

    public PublicLandingPageRepository(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public async Task<LandingPage?> GetPublishedBySlugAsync(string slug)
    {
        var filter = Builders<LandingPage>.Filter.Eq(landingPage => landingPage.Slug, slug);
        filter = Builders<LandingPage>.Filter.And(
            filter,
            Builders<LandingPage>.Filter.Eq(landingPage => landingPage.IsPublished, true)
        );

        return await _mongoDbContext.LandingPages.Find(filter).FirstOrDefaultAsync();
    }
}
