using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesenvolvendoApi.Data;
using DesenvolvendoApi.Models;
using DesenvolvendoApi.Data.Dtos;
using DesenvolvendoApi.Data.Dtos.Endereco;

namespace DesenvolvendoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class EnderecoController : ControllerBase
{
    private readonly FilmeContext _context;
    private readonly IMapper _mapper;

    public EnderecoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Retorna uma lista paginada de endereços.
    /// </summary>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Quantidade de itens por página (padrão: 10)</param>
    /// <returns>Lista de endereços</returns>
    /// <response code="200">Lista retornada com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ReadEnderecoDto>>> GetAllEnderecos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var enderecos = await _context.Enderecos
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var enderecosDto = _mapper.Map<List<ReadEnderecoDto>>(enderecos);

        return Ok(enderecosDto);
    }

    /// <summary>
    /// Retorna um endereço pelo Id.
    /// </summary>
    /// <param name="id">Id do endereço</param>
    /// <returns>Endereço encontrado</returns>
    /// <response code="200">Endereço encontrado</response>
    /// <response code="404">Endereço não encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadEnderecoDto>> GetEnderecoById(int id)
    {
        var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);

        if (endereco == null)
            return NotFound();

        var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

        return Ok(enderecoDto);
    }

    /// <summary>
    /// Cria um novo endereço.
    /// </summary>
    /// <param name="dto">Dados do endereço</param>
    /// <returns>Endereço criado</returns>
    /// <response code="201">Endereço criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadEnderecoDto>> CreateEndereco([FromBody] CreateEnderecoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var endereco = _mapper.Map<Endereco>(dto);

        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();

        var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

        return CreatedAtAction(nameof(GetEnderecoById), new { id = endereco.Id }, enderecoDto);
    }

    /// <summary>
    /// Atualiza completamente um endereço.
    /// </summary>
    /// <param name="id">Id do endereço</param>
    /// <param name="dto">Novos dados</param>
    /// <returns>Nenhum conteúdo</returns>
    /// <response code="204">Atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Endereço não encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEndereco(int id, [FromBody] UpdateEnderecoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);

        if (endereco == null)
            return NotFound();

        _mapper.Map(dto, endereco);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente um endereço.
    /// </summary>
    /// <param name="id">Id do endereço</param>
    /// <param name="dto">Campos a serem atualizados</param>
    /// <returns>Nenhum conteúdo</returns>
    /// <response code="204">Atualizado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Endereço não encontrado</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchEndereco(int id, [FromBody] UpdateEnderecoParcialDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var endereco = await _context.Enderecos.FindAsync(id);

        if (endereco == null)
            return NotFound();

        _mapper.Map(dto, endereco);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove um endereço pelo Id.
    /// </summary>
    /// <param name="id">Id do endereço</param>
    /// <returns>Nenhum conteúdo</returns>
    /// <response code="204">Removido com sucesso</response>
    /// <response code="404">Endereço não encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEndereco(int id)
    {
        var endereco = await _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id);

        if (endereco == null)
            return NotFound();

        _context.Enderecos.Remove(endereco);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}