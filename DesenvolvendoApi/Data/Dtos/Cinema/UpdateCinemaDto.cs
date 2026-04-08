using System.ComponentModel.DataAnnotations;

namespace DesenvolvendoApi.Data.Dtos.Cinema;

public class UpdateCinemaDto
{
    [Required(ErrorMessage = "O nome do cinema é obrigatório.")]
    public string Nome { get; set; }
}