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
    private FilmeContext _context { get; set; }
    public FilmesController(FilmeContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna uma lista paginada de filmes.
    /// </summary>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Quantidade de itens por página (padrão: 10)</param>
    /// <returns>Lista de filmes</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Filme>>> GetAllFilmes([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var filmesPaginados = await _context.Filmes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(filmesPaginados);
    }

    /// <summary>
    /// Cria um novo filme com base nos dados fornecidos no CreateFilmeDto. Retorna o filme criado com um status HTTP 201 Created.
    /// </summary>
    /// <param name="filmeDto">Objeto com os dados do filme a ser criado</param>
    /// <returns>Task<IActionResult></returns>
    /// <response code="201">Filme criado com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Filme>> CreateFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = filmeDto.ToFilme();
        _context.Filmes.Add(filme);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFilmeById), "Filmes", new { id = filme.Id }, filme);
    }

    /// <summary>
    /// Retorna um filme específico pelo seu identificador.
    /// </summary>
    /// <param name="id">Id do filme</param>
    /// <returns>Filme encontrado</returns>
    /// <response code="200">Filme encontrado com sucesso</response>
    /// <response code="404">Filme não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Filme>> GetFilmeById(int id)
    {
        var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id);

        if (filme == null)
            return NotFound();

        return Ok(filme);
    }

    /// <summary>
    /// Atualiza completamente os dados de um filme existente.
    /// </summary>
    /// <param name="id">Id do filme</param>
    /// <param name="filmeDto">Objeto com os novos dados do filme</param>
    /// <returns>Nenhum conteúdo</returns>
    /// <response code="204">Filme atualizado com sucesso</response>
    /// <response code="404">Filme não encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id);

        if (filme == null)
            return NotFound();

        filmeDto.UpdateEntity(filme);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente os dados de um filme.
    /// Apenas os campos informados serão modificados.
    /// </summary>
    /// <param name="id">Id do filme</param>
    /// <param name="dto">Objeto com os campos a serem atualizados</param>
    /// <returns>Nenhum conteúdo</returns>
    /// <response code="204">Filme atualizado com sucesso</response>
    /// <response code="404">Filme não encontrado</response>
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
    /// Remove um filme pelo seu identificador.
    /// </summary>
    /// <param name="id">Id do filme</param>
    /// <returns>Nenhum conteúdo</returns>
    /// <response code="204">Filme removido com sucesso</response>
    /// <response code="404">Filme não encontrado</response>
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
