using DesenvolvendoApi.Data.Dtos;
using DesenvolvendoApi.Models;

namespace DesenvolvendoApi.Data.Mappings
{
    public static class FilmeMappings
    {
        public static Filme ToFilme(this CreateFilmeDto createFilmeDto)
        {
            return new Filme
            {
                Titulo = createFilmeDto.Titulo,
                Genero = createFilmeDto.Genero,
                Duracao = createFilmeDto.Duracao
            };
        }
        public static void UpdateEntity(this UpdateFilmeDto dto, Filme filme)
        {
            filme.Titulo = dto.Titulo;
            filme.Genero = dto.Genero;
            filme.Duracao = dto.Duracao;
        }

    }
}