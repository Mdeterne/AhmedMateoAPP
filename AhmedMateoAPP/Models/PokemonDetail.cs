using System.Text.Json.Serialization;

namespace AhmedMateoAPP.Models;

public class PokemonDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("weight")]
    public int Weight { get; set; }

    [JsonPropertyName("sprites")]
    public PokemonSprites Sprites { get; set; } = new();

    // Propriété simple pour la description, peut être définie manuellement ou depuis l'API
    public string Description { get; set; } = string.Empty;

    // Propriété pour l'image, gère à la fois l'URL de l'API et un chemin de fichier local
    public string ImageUrl { get; set; } = string.Empty;

    // Constructeur pour la désérialisation JSON
    public PokemonDetail() { }

    // Méthode pour finaliser les données après la désérialisation
    public void FinalizeData()
    {
        // Si la description est vide (cas de l'API), on la génère
        if (string.IsNullOrWhiteSpace(Description))
        {
            Description = $"Le Pokémon {Name.ToUpper()} mesure {Height * 10} cm et pèse {Weight / 10.0} kg.";
        }

        // Si l'URL de l'image est vide, on utilise celle de l'API
        if (string.IsNullOrWhiteSpace(ImageUrl))
        {
            ImageUrl = Sprites?.FrontDefault ?? $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";
        }
    }
}

public class PokemonSprites
{
    [JsonPropertyName("front_default")]
    public string FrontDefault { get; set; } = string.Empty;
}
