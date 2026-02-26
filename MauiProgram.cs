using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BeerRate_MAUI_App.Data;
using BeerRate_MAUI_App.Services;
using BeerRate_MAUI_App.ViewModels;

namespace BeerRate_MAUI_App
{
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

            // Configure SQLite database
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "beerratings.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            // Register services
            builder.Services.AddSingleton<DatabaseService>();

            // Register ViewModels
            builder.Services.AddTransient<MainViewModel>();

            // Register Pages
            builder.Services.AddTransient<MainPage>();

            // Register App
            builder.Services.AddSingleton<App>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
