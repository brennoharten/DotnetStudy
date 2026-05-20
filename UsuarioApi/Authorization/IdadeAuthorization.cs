using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace UsuarioApi.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            if (!context.User.HasClaim(ClaimTypes.DateOfBirth, context.User.Claims.First(c => c.Type == ClaimTypes.DateOfBirth).Value))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var dataNascimento = DateTime.Parse(context.User.Claims.First(c => c.Type == ClaimTypes.DateOfBirth).Value);
            var idade = DateTime.Today.Year - dataNascimento.Year;

            if (dataNascimento > DateTime.Today.AddYears(-idade))
            {
                idade--;
            }

            if (idade >= requirement.Idade)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}