using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BeerRate_MAUI_App.Models;
using BeerRate_MAUI_App.Services;
using Microsoft.EntityFrameworkCore;

namespace BeerRate_MAUI_App.ViewModels
{
    [QueryProperty(nameof(BeerId), "beerId")]
    public partial class EditBeerViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private int beerId;

        [ObservableProperty]
        private string beerName = string.Empty;

        [ObservableProperty]
        private string brewery = string.Empty;

        [ObservableProperty]
        private string style = "IPA";

        [ObservableProperty]
        private int rating = 0;

        public List<string> BeerStyles { get; } = new()
        {
            "IPA",
            "Lager",
            "Stout",
            "Porter",
            "Wheat Beer",
            "Pilsner",
            "Ale",
            "Sour",
            "Amber",
            "Brown Ale",
            "Pale Ale",
            "Other"
        };

        public EditBeerViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task LoadBeerAsync()
        {
            var context = _databaseService.GetContext();
            var beer = await context.BeerRatings.FindAsync(BeerId);

            if (beer != null)
            {
                BeerName = beer.BeerName;
                Brewery = beer.Brewery;
                Style = beer.Style;
                Rating = beer.Rating;
            }
        }

        [RelayCommand]
        private void SetRating(string ratingValue)
        {
            if (int.TryParse(ratingValue, out int value))
            {
                Rating = value;
            }
        }

        [RelayCommand]
        private async Task SaveBeerAsync()
        {
            if (string.IsNullOrWhiteSpace(BeerName) || string.IsNullOrWhiteSpace(Brewery) || Rating == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all fields and select a rating", "OK");
                return;
            }

            try
            {
                var context = _databaseService.GetContext();
                var beer = await context.BeerRatings.FindAsync(BeerId);

                if (beer != null)
                {
                    beer.BeerName = BeerName;
                    beer.Brewery = Brewery;
                    beer.Style = Style;
                    beer.Rating = Rating;

                    await context.SaveChangesAsync();
                    await Shell.Current.DisplayAlert("Success", "Beer updated!", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to save: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
