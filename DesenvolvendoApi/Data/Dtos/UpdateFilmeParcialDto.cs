using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesenvolvendoApi.Data.Dtos;

public class UpdateFilmeParcialDto
{
    public string? Titulo { get; set; }
    public string? Genero { get; set; }
    public int? Duracao { get; set; }
}