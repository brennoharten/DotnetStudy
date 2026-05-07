
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Data.Dtos;
using UsuarioApi.Interfaces;
using UsuarioApi.Models;

namespace UsuarioApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly TokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task CadastraAsync(CadastroDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            var result = await _userManager.CreateAsync(usuario, usuarioDto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Failed to create user.");
            }
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Invalid login attempt.");
            }

            var usuario = await _userManager.FindByNameAsync(loginDto.Username);
            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}