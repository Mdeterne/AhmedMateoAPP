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

        // Injection des Services (Singletons pour garder l'état)
        builder.Services.AddSingleton<PokemonService>();
        builder.Services.AddSingleton<TeamService>();
        
        // Injection des ViewModels et Pages
        builder.Services.AddTransient<PokedexViewModel>();
        builder.Services.AddTransient<PokedexPage>();
        
        builder.Services.AddTransient<AddPokemonViewModel>();
        builder.Services.AddTransient<AddPokemonPage>();

        builder.Services.AddTransient<PokemonDetailViewModel>();
        builder.Services.AddTransient<PokemonDetailPage>();

        builder.Services.AddTransient<TeamViewModel>();
        builder.Services.AddTransient<TeamPage>();

        return builder.Build();
    }
}
