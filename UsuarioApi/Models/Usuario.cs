using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UsuarioApi.Models;

public class Usuario : IdentityUser
{
    public DateTime DataNascimento { get; set; }

    // 1x1 relationship with LandingPage stored in MongoDB
    public string? LandingPageId { get; set; }

    public Usuario() : base()
    {
    }

}