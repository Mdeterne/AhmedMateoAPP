using System.Windows.Input;
using AhmedMateoAPP.Models;
using AhmedMateoAPP.Services;

namespace AhmedMateoAPP.ViewModels;

public class PokemonDetailViewModel : BaseViewModel
{
    private readonly TeamService _teamService;
    private PokemonDetail _pokemon;

    public PokemonDetail Pokemon
    {
        get => _pokemon;
        set
        {
            SetProperty(ref _pokemon, value);
            OnPropertyChanged(nameof(FavoriteButtonText));
            OnPropertyChanged(nameof(FavoriteButtonColor));
        }
    }

    public string FavoriteButtonText => Pokemon?.IsFavorite == true ? "Retirer des favoris" : "Ajouter aux favoris";
    public Color FavoriteButtonColor => Pokemon?.IsFavorite == true ? Colors.Red : Colors.Green;

    public ICommand ToggleFavoriteCommand { get; }

    public PokemonDetailViewModel(TeamService teamService)
    {
        _teamService = teamService;
        
        ToggleFavoriteCommand = new Command(OnToggleFavorite);
    }

    private void OnToggleFavorite()
    {
        if (Pokemon != null)
        {
            _teamService.ToggleFavorite(Pokemon);
            
            // Forcer la mise à jour de l'UI pour le bouton
            OnPropertyChanged(nameof(FavoriteButtonText));
            OnPropertyChanged(nameof(FavoriteButtonColor));
        }
    }
}
