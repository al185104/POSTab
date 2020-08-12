using POSTab.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Converters
{
    class ListToCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ObservableCollection<ProductModel>))
            {
                throw new InvalidOperationException("The target must be a List");
            }

            ObservableCollection<ProductModel> items = (ObservableCollection<ProductModel>)value;
            return items.Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
