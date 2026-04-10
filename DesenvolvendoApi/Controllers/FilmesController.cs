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
[Produces("application/json")]
public class FilmesController : ControllerBase
{
    private readonly FilmeContext _context;

    public FilmesController(FilmeContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna uma lista paginada de filmes.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ReadFilmeDto>>> GetAllFilmes(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? nomeCinema = null)
    {
        var query = _context.Filmes
            .Include(f => f.Sessoes)
            .ThenInclude(s => s.Cinema) // 🔥 necessário pro filtro
            .AsQueryable();

        // 🔥 filtro opcional
        if (!string.IsNullOrWhiteSpace(nomeCinema))
        {
            query = query.Where(f =>
                f.Sessoes.Any(s => s.Cinema.Nome == nomeCinema));
        }

        var filmes = await query
            .Skip((page - 1) * pageSize) // 👈 paginação depois do filtro
            .Take(pageSize)
            .ToListAsync();

        var filmesDto = filmes.Select(f => f.ToDto()).ToList();

        return Ok(filmesDto);
    }

    /// <summary>
    /// Cria um novo filme.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadFilmeDto>> CreateFilme([FromBody] CreateFilmeDto filmeDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var filme = filmeDto.ToFilme();

        _context.Filmes.Add(filme);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFilmeById),
            new { id = filme.Id },
            filme.ToDto());
    }

    /// <summary>
    /// Retorna um filme por Id.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadFilmeDto>> GetFilmeById(int id)
    {
        var filme = await _context.Filmes
            .Include(f => f.Sessoes) // 🔥 importante
            .FirstOrDefaultAsync(f => f.Id == id);

        if (filme == null)
            return NotFound();

        return Ok(filme.ToDto());
    }

    /// <summary>
    /// Atualiza completamente um filme.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id);

        if (filme == null)
            return NotFound();

        filmeDto.UpdateEntity(filme);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente um filme.
    /// </summary>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Remove um filme.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
