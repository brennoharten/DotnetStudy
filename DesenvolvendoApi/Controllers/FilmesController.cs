using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesenvolvendoApi.Models;
using DesenvolvendoApi.Data;
using DesenvolvendoApi.Data.Dtos;
using DesenvolvendoApi.Data.Mappings;

namespace DesenvolvendoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmesController : ControllerBase
{
    private FilmeContext _context { get; set; }
    public FilmesController(FilmeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFilmes([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var filmesPaginados = await _context.Filmes.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return Ok(filmesPaginados);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = filmeDto.ToFilme();
        _context.Filmes.Add(filme);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFilmeById), "Filmes", new { id = filme.Id }, filme);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFilmeById(int id)
    {
        var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id);
        if (filme == null)
        {
            return NotFound();
        }
        return Ok(filme);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id);
        if (filme == null)
            return NotFound();
        filmeDto.UpdateEntity(filme);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchFilme(int id, [FromBody] UpdateFilmeParcialDto dto)
    {
        var filme = await _context.Filmes.FindAsync(id);

        if (filme == null)
            return NotFound();

        if (dto.Titulo is not null)
            filme.Titulo = dto.Titulo;

        if (dto.Genero is not null)
            filme.Genero = dto.Genero;

        if (dto.Duracao.HasValue)
            filme.Duracao = dto.Duracao.Value;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFilme(int id)
    {
        var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id);
        if (filme == null)
            return NotFound();
        _context.Filmes.Remove(filme);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
