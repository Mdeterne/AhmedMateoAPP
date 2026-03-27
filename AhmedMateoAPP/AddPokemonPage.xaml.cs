using AhmedMateoAPP.ViewModels;

namespace AhmedMateoAPP;

public partial class AddPokemonPage : ContentPage
{
    public AddPokemonPage(AddPokemonViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
