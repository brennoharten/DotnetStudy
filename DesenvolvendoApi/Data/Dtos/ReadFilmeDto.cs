using System.ComponentModel.DataAnnotations;
using DesenvolvendoApi.Data.Dtos.Sessao;

namespace DesenvolvendoApi.Data.Dtos;

public record ReadFilmeDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; }
    public List<ReadSessaoDto> Sessoes { get; set; }
}