using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Budget.Converter
{
    public class AmountToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal amount = (decimal)value;
            return amount == (decimal)0 ? Brushes.White :
                amount < (decimal)0 ? Brushes.Red : Brushes.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
