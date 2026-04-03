using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DesenvolvendoApi.Models;

namespace DesenvolvendoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{

    public List<Filme> Filmes { get; set; }
    private static int id = 0;

    [HttpGet]
    public async Task<IActionResult> GetAllFilmes([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var filmesPaginados = Filmes.Skip((page - 1) * pageSize).Take(pageSize);
        return Ok(filmesPaginados);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFilme([FromBody] Filme filme)
    {
        filme.Id = ++id;
        Filmes.Add(filme);
        return CreatedAtAction(nameof(GetFilmeByIdAsync), new {id = filme.Id}, filme);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> GetFilmeByIdAsync(string id)
    {
        var filme = Filmes.FirstOrDefault(f => f.Id == id);
        if (filme == null)
        {
            return NotFound();
        }
        return Ok(filme);
    }
}
