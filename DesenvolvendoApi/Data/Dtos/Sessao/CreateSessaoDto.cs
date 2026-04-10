using System.ComponentModel.DataAnnotations;

namespace DesenvolvendoApi.Data.Dtos.Sessao;

public record CreateSessaoDto
{
    [Required]
    public int FilmeId { get; set; }
    
    [Required]
    public int CinemaId { get; set; }
}