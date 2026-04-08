using DesenvolvendoApi.Data.Dtos.Endereco;

namespace DesenvolvendoApi.Data.Dtos.Cinema;

public class ReadCinemaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ReadEnderecoDto EderecoDto { get; set;}
}