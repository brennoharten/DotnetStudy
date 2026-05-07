using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsuarioApi.Data.Dtos;

namespace UsuarioApi.Interfaces
{
    public interface IUsuarioService
    {
        public Task CadastraAsync(CadastroDto usuarioDto);
        public Task<string> LoginAsync(LoginDto loginDto);
    }
}