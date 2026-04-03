using System.Text.Json.Serialization;

namespace Study;

public class Song
{

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("culture")]
    public string Culture { get; set; }

    [JsonPropertyName("born")]
    public string Born { get; set; }

    [JsonPropertyName("died")]
    public string Died { get; set; }

    [JsonPropertyName("titles")]
    public List<string> Titles { get; set; }

    [JsonPropertyName("aliases")]
    public List<string> Aliases { get; set; }

    [JsonPropertyName("playedBy")]
    public List<string> PlayedBy { get; set; }
}
