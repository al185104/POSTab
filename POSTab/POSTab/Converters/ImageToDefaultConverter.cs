using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Converters
{
    class ImageToDefaultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                throw new InvalidOperationException("The target must be a Date");
            }

            if (string.IsNullOrWhiteSpace(value as string) || value.ToString().Equals("string") || value.ToString().Contains(".png") == false)
                return "settings_productimage_placeholder.png";
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
