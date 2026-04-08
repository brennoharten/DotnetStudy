

namespace DesenvolvendoApi.Models;

public class Endereco
{
    public int Id { get; set; }
    public string Lugradouro { get; set; }
    public int Numero { get; set; }
    public virtual Cinema Cinema { get; set; }
}