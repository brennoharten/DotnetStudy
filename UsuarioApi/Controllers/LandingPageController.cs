using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Data.Dtos.LandingPage;
using UsuarioApi.Models;
using UsuarioApi.Services;

namespace UsuarioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LandingPageController : ControllerBase
    {
        private readonly ILandingPageService _landingPageService;
        private readonly UserManager<Usuario> _userManager;

        public LandingPageController(
            ILandingPageService landingPageService,
            UserManager<Usuario> userManager)
        {
            _landingPageService = landingPageService;
            _userManager = userManager;
        }

        /// <summary>
        /// Criar nova landing page para o usuário autenticado
        /// </summary>
        [HttpPost("criar")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateLandingPageDto dto)
        {
            try
            {
                var usuario = await _userManager.GetUserAsync(User);
                if (usuario == null)
                    return Unauthorized("Usuário não autenticado.");

                var result = await _landingPageService.CreateAsync(usuario.Id, dto);
                return CreatedAtAction(nameof(GetByUsuarioId), result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Obter landing page do usuário autenticado
        /// </summary>
        [HttpGet("minha-landing")]
        public async Task<IActionResult> GetByUsuarioId()
        {
            try
            {
                var usuario = await _userManager.GetUserAsync(User);
                if (usuario == null)
                    return Unauthorized("Usuário não autenticado.");

                var result = await _landingPageService.GetByUsuarioIdAsync(usuario.Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Obter landing page por ID (pode ser pública)
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _landingPageService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar landing page do usuário autenticado
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CreateLandingPageDto dto)
        {
            try
            {
                var usuario = await _userManager.GetUserAsync(User);
                if (usuario == null)
                    return Unauthorized("Usuário não autenticado.");

                var result = await _landingPageService.UpdateAsync(usuario.Id, id, dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deletar landing page do usuário autenticado
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var usuario = await _userManager.GetUserAsync(User);
                if (usuario == null)
                    return Unauthorized("Usuário não autenticado.");

                var result = await _landingPageService.DeleteAsync(usuario.Id, id);
                if (result)
                    return Ok(new { mensagem = "Landing page deletada com sucesso." });

                return BadRequest(new { mensagem = "Erro ao deletar landing page." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
