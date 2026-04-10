using DesenvolvendoApi.Data.Dtos.Endereco;
using DesenvolvendoApi.Data.Dtos.Sessao;

namespace DesenvolvendoApi.Data.Dtos.Cinema;

public class ReadCinemaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ReadEnderecoDto EnderecoDto { get; set;}
    public List<ReadSessaoDto> Sessoes { get; set;}
}