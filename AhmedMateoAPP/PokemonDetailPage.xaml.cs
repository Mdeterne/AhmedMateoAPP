using AhmedMateoAPP.Models;
using AhmedMateoAPP.ViewModels;

namespace AhmedMateoAPP;

[QueryProperty(nameof(Pokemon), "Pokemon")]
public partial class PokemonDetailPage : ContentPage
{
    private readonly PokemonDetailViewModel _viewModel;
    private PokemonDetail _pokemon;

    public PokemonDetail Pokemon
    {
        get => _pokemon;
        set
        {
            _pokemon = value;
            _viewModel.Pokemon = value; // Assigner au ViewModel
        }
    }

    public PokemonDetailPage(PokemonDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}
