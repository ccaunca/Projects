using System;
using System.Globalization;
using System.Windows.Data;

namespace Budget.Converter
{
    public class AmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string rawAmount = value.ToString();
            return "$" + rawAmount;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dollarAmount = value.ToString();
            return dollarAmount.Remove(0, 1);
        }
    }
}
