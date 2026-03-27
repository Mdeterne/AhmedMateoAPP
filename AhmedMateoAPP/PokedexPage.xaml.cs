using AhmedMateoAPP.ViewModels;

namespace AhmedMateoAPP;

public partial class PokedexPage : ContentPage
{
    private readonly PokedexViewModel _viewModel;

    public PokedexPage(PokedexViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Charger les Pokémon si la liste est vide
        if (_viewModel.Pokemons.Count == 0)
        {
            _viewModel.LoadPokemonsCommand.Execute(null);
        }
    }
}
