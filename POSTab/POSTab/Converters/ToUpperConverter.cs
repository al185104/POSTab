using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Converters
{
    class ToUpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (!(value is string))
                {
                    throw new InvalidOperationException("The target must be a string");
                }

                var str = value as string;
                return string.IsNullOrEmpty(str) ? string.Empty : str.ToUpper();
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
