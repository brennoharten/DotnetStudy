using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Data.Dtos;
using UsuarioApi.Interfaces;
using UsuarioApi.Models;

namespace UsuarioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> CriarUsuario([FromBody] CadastroDto usuarioDto)
    {
        await _usuarioService.CadastraAsync(usuarioDto);
        return Ok("Usuário criado com sucesso.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUsuario([FromBody] LoginDto loginDto)
    {
        var token = await _usuarioService.LoginAsync(loginDto);
        return Ok(token);
    }
}