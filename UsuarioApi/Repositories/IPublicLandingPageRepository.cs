using System.Threading.Tasks;
using UsuarioApi.Infrastructure.Mongo.Models;

namespace UsuarioApi.Repositories;

public interface IPublicLandingPageRepository
{
    Task<LandingPage?> GetPublishedBySlugAsync(string slug);
}
