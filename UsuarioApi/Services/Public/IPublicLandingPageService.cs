using System.Threading.Tasks;
using UsuarioApi.Data.Dtos.Public;

namespace UsuarioApi.Services.Public;

public interface IPublicLandingPageService
{
    Task<PublicLandingPageDto> GetBySlugAsync(string slug);
}
