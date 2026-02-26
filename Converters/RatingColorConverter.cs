using System.Globalization;

namespace BeerRate_MAUI_App.Converters
{
    public class RatingColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int currentRating && parameter is string ratingStr && int.TryParse(ratingStr, out int buttonRating))
            {
                return currentRating == buttonRating ? Colors.Orange : Colors.LightGray;
            }
            return Colors.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
