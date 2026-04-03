using System.Linq;
using Models;

namespace Study
{
    internal class LinqFilters
    {
        public static void AllGenres(List<Musica> lista)
        {
            var allGenres = lista.Select(l => l.Genero).Distinct().ToList();

            foreach (var genre in allGenres)
            {
                Console.WriteLine($"-> {genre}");
            }
        }
        public static void AllArtistsOrdered(List<Musica> lista)
        {
            var allNames = lista.OrderBy(x => x.Artista).Select(x => x.Artista).Distinct().ToList();
            foreach (var name in allNames)
            {
                Console.WriteLine($"-> {name}");
            }
            
        }
        public static void AllArtistsOrdered(List<Musica> lista, string genre)
        {
            var allNames = lista.Where(y => y.Genero.Contains(genre)).Select(x => x.Artista).Distinct().ToList();
            foreach (var name in allNames)
            {
                Console.WriteLine($"-> {name}");
            }
            
        }
        public static void AllSongsFromArtist(List<Musica> lista, string artist)
        {
            var allSongs = lista.Where(y => y.Artista.Equals(artist)).Select(x => x.Nome).ToList();
            foreach (var name in allSongs)
            {
                Console.WriteLine($"-> {name}");
            }
            
        }
    }
}