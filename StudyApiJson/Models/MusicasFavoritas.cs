using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace StudyApiJson.Models
{
    internal class MusicasFavoritas
    {
        public string Name { get; set; }

        public List<Musica> MinhasMusicas { get; }

        public void AdicionarMusica(Musica musica)
        {
            MinhasMusicas.Add(musica);
        }

        public void ExibirMusicas()
        {
            Console.WriteLine($"Musicas favoritas do {Name}");
            foreach (var musica in MinhasMusicas)
            {
                System.Console.WriteLine($"-> {musica.Nome} do artista {musica.Artista}");
            }
        }

        public void GerarArquivo()
        {
            string nomeDoArquivo = "arquivo.json";
            string json = JsonSerializer.Serialize(new
            {
                nome = Name,
                musicas = MinhasMusicas
            });

            File.WriteAllText(nomeDoArquivo, json);
        }

        public void GerarArquivoStream()
        {
            string nomeDoArquivo = "arquivo.json";

            using FileStream fs = File.Create(nomeDoArquivo);
            using Utf8JsonWriter writer = new Utf8JsonWriter(fs, new JsonWriterOptions
            {
                Indented = true
            });

            writer.WriteStartObject();
            writer.WriteString("nome", Name);

            writer.WriteStartArray("musicas");
            foreach (var musica in MinhasMusicas)
            {
                writer.WriteStringValue(musica.Artista);
            }
            writer.WriteEndArray();

            writer.WriteEndObject();
        }


    }
}