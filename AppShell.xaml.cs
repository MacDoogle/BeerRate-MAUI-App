using BeerRate_MAUI_App.Pages;

namespace BeerRate_MAUI_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register route for navigation
            Routing.RegisterRoute("editbeer", typeof(EditBeerPage));
        }
    }
}
