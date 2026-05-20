using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace UsuarioApi.Authorization
{
    public class IdadeMinima : IAuthorizationRequirement
    {
        public int Idade;

        public IdadeMinima(int idade)
        {
            this.Idade = idade;
        }
    }
}