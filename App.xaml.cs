using BeerRate_MAUI_App.Services;

namespace BeerRate_MAUI_App
{
    public partial class App : Application
    {
        public App(DatabaseService databaseService)
        {
            InitializeComponent();

            MainPage = new AppShell();

            // Initialize database on startup
            InitializeDatabase(databaseService);
        }

        private async void InitializeDatabase(DatabaseService databaseService)
        {
            await databaseService.InitializeAsync();
        }
    }
}
