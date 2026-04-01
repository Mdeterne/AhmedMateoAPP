using System.Collections.ObjectModel;
using System.Windows.Input;
using AhmedMateoAPP.Models;
using AhmedMateoAPP.Services;

namespace AhmedMateoAPP.ViewModels;

public class TeamViewModel : BaseViewModel, IDisposable
{
    private readonly TeamService _teamService;

    public ObservableCollection<PokemonDetail> Favorites => _teamService.Favorites;
    public ObservableCollection<PokemonDetail> Team => _teamService.Team;

    public ICommand AddToTeamCommand { get; }
    public ICommand RemoveFromTeamCommand { get; }

    public TeamViewModel(TeamService teamService)
    {
        _teamService = teamService;
        Title = "Mon Équipe";

        AddToTeamCommand = new Command<PokemonDetail>(OnAddToTeam);
        RemoveFromTeamCommand = new Command<PokemonDetail>(OnRemoveFromTeam);

        // S'abonner aux changements pour forcer le rafraîchissement si nécessaire
        _teamService.OnFavoritesChanged += RefreshUI;
        _teamService.OnTeamChanged += RefreshUI;
    }

    private void OnAddToTeam(PokemonDetail pokemon)
    {
        if (pokemon == null) return;

        bool success = _teamService.AddToTeam(pokemon);
        
        if (!success)
        {
            if (Team.Count >= 6)
            {
                Application.Current.MainPage.DisplayAlert("Équipe pleine", "Votre équipe est déjà composée de 6 Pokémon. Retirez-en un pour en ajouter un nouveau.", "OK");
            }
            else if (Team.Any(p => p.Id == pokemon.Id))
            {
                Application.Current.MainPage.DisplayAlert("Déjà présent", "Ce Pokémon est déjà dans votre équipe.", "OK");
            }
        }
    }

    private void OnRemoveFromTeam(PokemonDetail pokemon)
    {
        if (pokemon == null) return;
        _teamService.RemoveFromTeam(pokemon);
    }

    private void RefreshUI()
    {
        // Notifier la vue que les listes ont pu changer
        OnPropertyChanged(nameof(Favorites));
        OnPropertyChanged(nameof(Team));
    }

    public void Dispose()
    {
        _teamService.OnFavoritesChanged -= RefreshUI;
        _teamService.OnTeamChanged -= RefreshUI;
    }
}
