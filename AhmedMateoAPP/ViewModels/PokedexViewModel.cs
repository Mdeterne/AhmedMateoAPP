using System.Collections.ObjectModel;
using System.Windows.Input;
using AhmedMateoAPP.Models;
using AhmedMateoAPP.Services;

namespace AhmedMateoAPP.ViewModels;

public class PokedexViewModel : BaseViewModel, IDisposable
{
    private readonly PokemonService _pokemonService;
    private List<PokemonDetail> _allPokemons = new();
    
    public ObservableCollection<PokemonDetail> Pokemons { get; } = new();
    
    public ICommand SearchCommand { get; }
    public ICommand LoadPokemonsCommand { get; }
    public ICommand GoToDetailsCommand { get; }

    public PokedexViewModel(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
        Title = "Pokédex";
        
        SearchCommand = new Command<string>(FilterPokemons);
        LoadPokemonsCommand = new Command(async () => await LoadPokemonsAsync());
        GoToDetailsCommand = new Command<PokemonDetail>(async (pokemon) => await GoToDetailsAsync(pokemon));

        // S'abonner aux événements d'ajout de Pokémon
        _pokemonService.OnPokemonAdded += OnPokemonAdded;
    }

    private void OnPokemonAdded(PokemonDetail newPokemon)
    {
        // S'assurer que la mise à jour de l'UI se fait sur le thread principal
        MainThread.BeginInvokeOnMainThread(() =>
        {
            _allPokemons.Insert(0, newPokemon);
            Pokemons.Insert(0, newPokemon);
        });
    }

    private async Task GoToDetailsAsync(PokemonDetail pokemon)
    {
        if (pokemon == null) return;

        await Shell.Current.GoToAsync(nameof(PokemonDetailPage), true, new Dictionary<string, object>
        {
            { "Pokemon", pokemon }
        });
    }

    private async Task LoadPokemonsAsync()
    {
        if (IsBusy || _allPokemons.Any()) return;
        IsBusy = true;

        try
        {
            _allPokemons = await _pokemonService.GetPokemonsAsync();
            FilterPokemons(string.Empty);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void FilterPokemons(string query)
    {
        Pokemons.Clear();
        var filtered = string.IsNullOrWhiteSpace(query)
            ? _allPokemons
            : _allPokemons.Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
        
        foreach (var pokemon in filtered)
        {
            Pokemons.Add(pokemon);
        }
    }

    public void Dispose()
    {
        // Se désabonner pour éviter les fuites de mémoire
        _pokemonService.OnPokemonAdded -= OnPokemonAdded;
    }
}
