using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace POSTab.Models
{
    public class CardWrapperModel
    {
        public string CardBGColor { get; set; }
        public ObservableCollection<ProductModel> Category { get; set; }
    }
}
