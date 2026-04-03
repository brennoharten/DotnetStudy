using System;
using System.IO;
using System.Text.Json;
using Models;
using Study;

using (HttpClient client = new HttpClient())
{
    try
    {
        var resultado = await client.GetStringAsync("https://guilhermeonrails.github.io/api-csharp-songs/songs.json");
        var musicas = JsonSerializer.Deserialize<List<Musica>>(resultado);

        var musicasGroupedByArtista = musicas.GroupBy(m => m.Artista).Take(5)
            .Select(g => new
            {
                Artista = g.Key,
                Musicas = g.ToList(),
                Total = g.Count(),
            }).ToList();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var caminhoArquivo = Path.Combine(Environment.CurrentDirectory, "musicas-agrupadas.json");

        var json = JsonSerializer.Serialize( musicasGroupedByArtista, options);

        await File.WriteAllTextAsync(caminhoArquivo, json);


        /* LinqFilters.AllSongsFromArtist(musicas!, "The Goo Goo Dolls");

        await foreach(var linha in File.ReadLineAsync("caminho.csv")){
            var partes = linha.Split(";");
        }

        using var reader = new StreamReader("caminho.csv");
        while (!reader.EndOfStream)        
        {   
            var linha = await reader.ReadLineAsync();
            var partes = linha.Split(";");
        } */

    }
    catch (Exception ex)
    {
        Console.WriteLine($"deu pau! \n {ex.Message}");
        throw ex;
    }
}
