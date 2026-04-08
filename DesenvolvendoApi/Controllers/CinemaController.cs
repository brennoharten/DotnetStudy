using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesenvolvendoApi.Data;
using DesenvolvendoApi.Models;
using DesenvolvendoApi.Data.Dtos.Cinema;

namespace DesenvolvendoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class CinemaController : ControllerBase
{
    private readonly FilmeContext _context;
    private readonly IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Retorna uma lista paginada de cinemas.
    /// </summary>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Quantidade de itens por página (padrão: 10)</param>
    /// <returns>Lista de cinemas</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ReadCinemaDto>>> GetAllCinemas([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var cinemas = await _context.Cinemas
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var cinemasDto = _mapper.Map<List<ReadCinemaDto>>(cinemas);

        return Ok(cinemasDto);
    }

    /// <summary>
    /// Retorna um cinema pelo Id.
    /// </summary>
    /// <param name="id">Id do cinema</param>
    /// <returns>Cinema encontrado</returns>
    /// <response code="200">Cinema encontrado</response>
    /// <response code="404">Cinema não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadCinemaDto>> GetCinemaById(int id)
    {
        var cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);

        if (cinema == null)
            return NotFound();

        var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

        return Ok(cinemaDto);
    }

    /// <summary>
    /// Cria um novo cinema.
    /// </summary>
    /// <param name="dto">Dados do cinema</param>
    /// <returns>Cinema criado</returns>
    /// <response code="201">Cinema criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadCinemaDto>> CreateCinema([FromBody] CreateCinemaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cinema = _mapper.Map<Cinema>(dto);

        _context.Cinemas.Add(cinema);
        await _context.SaveChangesAsync();

        var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);

        return CreatedAtAction(nameof(GetCinemaById), new { id = cinema.Id }, cinemaDto);
    }

    /// <summary>
    /// Atualiza completamente um cinema.
    /// </summary>
    /// <param name="id">Id do cinema</param>
    /// <param name="dto">Novos dados</param>
    /// <returns>Nenhum conteúdo</returns>
    /// <response code="204">Atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Cinema não encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCinema(int id, [FromBody] UpdateCinemaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);

        if (cinema == null)
            return NotFound();

        _mapper.Map(dto, cinema);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente um cinema.
    /// </summary>
    /// <param name="id">Id do cinema</param>
    /// <param name="dto">Campos a serem atualizados</param>
    /// <returns>Nenhum conteúdo</returns>
    /// <response code="204">Atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Cinema não encontrado</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchCinema(int id, [FromBody] UpdateCinemaParcialDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var cinema = await _context.Cinemas.FindAsync(id);

        if (cinema == null)
            return NotFound();

        _mapper.Map(dto, cinema);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove um cinema pelo Id.
    /// </summary>
    /// <param name="id">Id do cinema</param>
    /// <returns>Nenhum conteúdo</returns>
    /// <response code="204">Removido com sucesso</response>
    /// <response code="404">Cinema não encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCinema(int id)
    {
        var cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);

        if (cinema == null)
            return NotFound();

        _context.Cinemas.Remove(cinema);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}