using System.Net.Http.Json;
using AhmedMateoAPP.Models;

namespace AhmedMateoAPP.Services;

public class PokemonService
{
    private readonly HttpClient _httpClient;
    private List<PokemonDetail> _pokemons = new();
    private bool _isDataLoaded = false;

    // Événement pour notifier l'ajout d'un nouveau Pokémon
    public event Action<PokemonDetail> OnPokemonAdded;

    public PokemonService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/")
        };
    }

    public async Task<List<PokemonDetail>> GetPokemonsAsync()
    {
        if (!_isDataLoaded)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<PokemonApiResponse>("pokemon?limit=151");
                if (response?.Results != null)
                {
                    var tasks = response.Results.Select(async r => await GetPokemonDetailAsync(r.Name));
                    var details = await Task.WhenAll(tasks);
                    _pokemons = details.Where(d => d != null).ToList();
                    _isDataLoaded = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la récupération des Pokémon : {ex.Message}");
            }
        }
        return _pokemons;
    }

    public async Task<PokemonDetail?> GetPokemonDetailAsync(string nameOrId)
    {
        try
        {
            var detail = await _httpClient.GetFromJsonAsync<PokemonDetail>($"pokemon/{nameOrId.ToLower()}");
            detail?.FinalizeData();
            return detail;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des détails pour {nameOrId} : {ex.Message}");
            return null;
        }
    }

    public void AddPokemon(PokemonDetail pokemon)
    {
        if (pokemon == null) return;

        // Simuler un ID unique pour les nouveaux Pokémon
        pokemon.Id = _pokemons.Count + 10000; 
        
        _pokemons.Insert(0, pokemon); // Ajouter au début de la liste
        
        // Déclencher l'événement pour notifier les abonnés
        OnPokemonAdded?.Invoke(pokemon);
    }
}
