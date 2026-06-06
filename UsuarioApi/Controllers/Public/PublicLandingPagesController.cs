using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Services.Public;

namespace UsuarioApi.Controllers.Public;

[ApiController]
[Route("api/public/landing-pages")]
[AllowAnonymous]
public class PublicLandingPagesController : ControllerBase
{
    private readonly IPublicLandingPageService _publicLandingPageService;

    public PublicLandingPagesController(IPublicLandingPageService publicLandingPageService)
    {
        _publicLandingPageService = publicLandingPageService;
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        try
        {
            var landingPage = await _publicLandingPageService.GetBySlugAsync(slug);
            return Ok(landingPage);
        }
        catch (Exception)
        {
            return NotFound(new { mensagem = "Landing page não encontrada." });
        }
    }
}
