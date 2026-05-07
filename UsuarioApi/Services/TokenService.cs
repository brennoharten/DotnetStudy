using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using UsuarioApi.Models;

namespace UsuarioApi.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Name, usuario.UserName)
            });

            // Aqui você pode usar uma biblioteca como System.IdentityModel.Tokens.Jwt para gerar o token JWT
            var token = new JwtSecurityToken(
                    claims: claimsIdentity.Claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("afdgafdgfsfdgsthrthsfsdfgsdrfgSAFDGSe")), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}