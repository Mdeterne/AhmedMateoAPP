using AhmedMateoAPP.Models;

namespace AhmedMateoAPP;

[QueryProperty(nameof(Pokemon), "Pokemon")]
public partial class PokemonDetailPage : ContentPage
{
    private PokemonDetail _pokemon;
    public PokemonDetail Pokemon
    {
        get => _pokemon;
        set
        {
            _pokemon = value;
            OnPropertyChanged();
            BindingContext = _pokemon; // Mettre à jour le contexte de liaison
        }
    }

    public PokemonDetailPage()
    {
        InitializeComponent();
    }
}
