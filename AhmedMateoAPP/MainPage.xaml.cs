using AhmedMateoAPP.Models;
using System.Collections.ObjectModel;

namespace AhmedMateoAPP;

public partial class MainPage : ContentPage
{
    public ObservableCollection<PokemonItem> FeaturedPokemon { get; set; }

    public MainPage()
    {
        InitializeComponent();
        
        // Mock data for Carousel
        FeaturedPokemon = new ObservableCollection<PokemonItem>
        {
            new PokemonItem { Name = "Pikachu", ImageUrl = "pikachu.png" },
            new PokemonItem { Name = "Charmander", ImageUrl = "charmander.png" },
            new PokemonItem { Name = "Bulbasaur", ImageUrl = "bulbasaur.png" }
        };

        FeaturedCarousel.ItemsSource = FeaturedPokemon;
    }

    private async void OnShowGifClicked(object sender, EventArgs e)
    {
        // Navigate to the GIF page
        await Navigation.PushAsync(new GifPage());
    }
}
