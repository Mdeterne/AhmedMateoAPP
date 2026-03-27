using Microsoft.Extensions.Logging;
using AhmedMateoAPP.Services;
using AhmedMateoAPP.ViewModels;

namespace AhmedMateoAPP;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Injection des dépendances
        builder.Services.AddSingleton<PokemonService>();
        
        builder.Services.AddTransient<PokedexViewModel>();
        builder.Services.AddTransient<PokedexPage>();
        
        builder.Services.AddTransient<AddPokemonViewModel>();
        builder.Services.AddTransient<AddPokemonPage>();

        return builder.Build();
    }
}
