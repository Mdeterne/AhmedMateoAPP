namespace AhmedMateoAPP;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        // Enregistrer la route pour la page de détails
        Routing.RegisterRoute(nameof(PokemonDetailPage), typeof(PokemonDetailPage));
    }
}
