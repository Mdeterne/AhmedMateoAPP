using System.Text.Json.Serialization;

namespace AhmedMateoAPP.Models;

public class PokemonApiResponse
{
    [JsonPropertyName("results")]
    public List<PokemonApiResult> Results { get; set; } = new();
}

public class PokemonApiResult
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
