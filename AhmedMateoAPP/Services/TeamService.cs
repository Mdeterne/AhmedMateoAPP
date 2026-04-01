using System.Collections.ObjectModel;
using AhmedMateoAPP.Models;

namespace AhmedMateoAPP.Services;

public class TeamService
{
    public ObservableCollection<PokemonDetail> Favorites { get; } = new();
    public ObservableCollection<PokemonDetail> Team { get; } = new();

    public event Action OnFavoritesChanged;
    public event Action OnTeamChanged;

    public void ToggleFavorite(PokemonDetail pokemon)
    {
        pokemon.IsFavorite = !pokemon.IsFavorite;

        if (pokemon.IsFavorite)
        {
            if (!Favorites.Any(p => p.Id == pokemon.Id))
            {
                Favorites.Add(pokemon);
            }
        }
        else
        {
            var existing = Favorites.FirstOrDefault(p => p.Id == pokemon.Id);
            if (existing != null)
            {
                Favorites.Remove(existing);
            }
            // S'il est retiré des favoris, il doit aussi être retiré de l'équipe
            RemoveFromTeam(pokemon);
        }
        OnFavoritesChanged?.Invoke();
    }

    public bool AddToTeam(PokemonDetail pokemon)
    {
        if (Team.Count >= 6 || Team.Any(p => p.Id == pokemon.Id) || !pokemon.IsFavorite)
        {
            return false; // Équipe pleine, déjà dans l'équipe, ou pas un favori
        }
        
        Team.Add(pokemon);
        OnTeamChanged?.Invoke();
        return true;
    }

    public void RemoveFromTeam(PokemonDetail pokemon)
    {
        var existing = Team.FirstOrDefault(p => p.Id == pokemon.Id);
        if (existing != null)
        {
            Team.Remove(existing);
            OnTeamChanged?.Invoke();
        }
    }
}
