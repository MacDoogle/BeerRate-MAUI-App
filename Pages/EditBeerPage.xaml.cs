using BeerRate_MAUI_App.ViewModels;

namespace BeerRate_MAUI_App.Pages
{
    public partial class EditBeerPage : ContentPage
    {
        private readonly EditBeerViewModel _viewModel;

        public EditBeerPage(EditBeerViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadBeerAsync();
        }
    }
}
