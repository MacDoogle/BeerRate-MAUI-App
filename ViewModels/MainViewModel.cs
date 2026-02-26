using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BeerRate_MAUI_App.Models;
using BeerRate_MAUI_App.Services;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace BeerRate_MAUI_App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private string beerName = string.Empty;

        [ObservableProperty]
        private string brewery = string.Empty;

        [ObservableProperty]
        private string style = "IPA";

        [ObservableProperty]
        private int rating = 0;

        [RelayCommand]
        private void SetRating(string ratingValue)
        {
            if (int.TryParse(ratingValue, out int value))
            {
                Rating = value;
            }
        }

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private ObservableCollection<BeerRating> beerRatings = new();

        [ObservableProperty]
        private ObservableCollection<BeerRating> filteredBeerRatings = new();

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

        public MainViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task InitializeAsync()
        {
            await LoadBeersAsync();
        }

        [RelayCommand]
        private async Task AddBeerAsync()
        {
            if (string.IsNullOrWhiteSpace(BeerName) || string.IsNullOrWhiteSpace(Brewery) || Rating == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all fields and select a rating", "OK");
                return;
            }

            var newBeer = new BeerRating
            {
                BeerName = BeerName,
                Brewery = Brewery,
                Style = Style,
                Rating = Rating
            };

            var context = _databaseService.GetContext();
            context.BeerRatings.Add(newBeer);
            await context.SaveChangesAsync();

            // Clear form
            BeerName = string.Empty;
            Brewery = string.Empty;
            Style = "IPA";
            Rating = 0;

            await LoadBeersAsync();
            await Shell.Current.DisplayAlert("Success", "Beer rating added!", "OK");
        }

        [RelayCommand]
        private async Task LoadBeersAsync()
        {
            var context = _databaseService.GetContext();
            var beers = await context.BeerRatings
                .OrderByDescending(b => b.Id)
                .ToListAsync();

            BeerRatings.Clear();
            foreach (var beer in beers)
            {
                BeerRatings.Add(beer);
            }

            FilterBeers();
        }

        partial void OnSearchTextChanged(string value)
        {
            FilterBeers();
        }

        private void FilterBeers()
        {
            FilteredBeerRatings.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? BeerRatings
                : BeerRatings.Where(b =>
                    b.BeerName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    b.Brewery.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    b.Style.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            foreach (var beer in filtered)
            {
                FilteredBeerRatings.Add(beer);
            }
        }

        [RelayCommand]
        private async Task ExportToCsvAsync()
        {
            try
            {
                var csv = "Beer Name,Brewery,Style,Rating\n";
                foreach (var beer in BeerRatings)
                {
                    csv += $"\"{beer.BeerName}\",\"{beer.Brewery}\",\"{beer.Style}\",{beer.Rating}\n";
                }

                var fileName = $"BeerRatings_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
                await File.WriteAllTextAsync(filePath, csv);

                await Share.Default.RequestAsync(new ShareFileRequest
                {
                    Title = "Export Beer Ratings",
                    File = new ShareFile(filePath)
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to export: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task ViewStatsAsync()
        {
            var totalBeers = BeerRatings.Count;
            var mostPopularStyle = BeerRatings
                .GroupBy(b => b.Style)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "No ratings yet";
            var mostRatedBrewery = BeerRatings
                .GroupBy(b => b.Brewery)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "No ratings yet";
            var ratingCounts = BeerRatings
                .GroupBy(b => b.Rating)
                .OrderBy(g => g.Key)
                .Select(g => $"{g.Key} Stars: {g.Count()}")
                .ToList();

            var statsMessage = $"Total Beers: {totalBeers}\n" +
                             $"Most Popular Style: {mostPopularStyle}\n" +
                             $"Most Rated Brewery: {mostRatedBrewery}\n\n" +
                             $"Rating Distribution:\n{string.Join("\n", ratingCounts)}";

            await Shell.Current.DisplayAlert("?? Statistics", statsMessage, "OK");
        }
    }
}
