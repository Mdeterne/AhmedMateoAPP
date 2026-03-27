using System.Windows.Input;
using AhmedMateoAPP.Models;
using AhmedMateoAPP.Services;

namespace AhmedMateoAPP.ViewModels;

public class AddPokemonViewModel : BaseViewModel
{
    private readonly PokemonService _pokemonService;

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set
        {
            SetProperty(ref _name, value);
            ((Command)SaveCommand).ChangeCanExecute(); // Réévaluer si on peut sauvegarder
        }
    }

    private string _description = string.Empty;
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private string _imageUrl = string.Empty;
    public string ImageUrl
    {
        get => _imageUrl;
        set => SetProperty(ref _imageUrl, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public AddPokemonViewModel(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
        Title = "Ajouter un Pokémon";

        // La commande de sauvegarde n'est active que si le nom est renseigné
        SaveCommand = new Command(OnSave, CanSave);
        CancelCommand = new Command(OnCancel);
    }

    private bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }

    private async void OnSave()
    {
        var newPokemon = new PokemonDetail
        {
            Name = Name.Trim(),
            Description = Description.Trim(),
            ImageUrl = string.IsNullOrWhiteSpace(ImageUrl) ? "dotnet_bot.png" : ImageUrl.Trim() // Image par défaut si vide
        };

        // Ajouter au service
        _pokemonService.AddPokemon(newPokemon);

        // Optionnel : Afficher un message de succès
        await Application.Current.MainPage.DisplayAlert("Succès", $"{Name} a été ajouté au Pokédex !", "OK");

        // Réinitialiser le formulaire
        Name = string.Empty;
        Description = string.Empty;
        ImageUrl = string.Empty;
    }

    private void OnCancel()
    {
        // Réinitialiser le formulaire sans sauvegarder
        Name = string.Empty;
        Description = string.Empty;
        ImageUrl = string.Empty;
    }
}
