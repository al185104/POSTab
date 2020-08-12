using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace POSTab.Models
{
    public class ApplicationProductList
    {
        public int Count { get; set; }
        public ObservableCollection<ProductModel> Items { get; set; } = new ObservableCollection<ProductModel>();
    }
}
