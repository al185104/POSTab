using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace POSTab.Models
{
    public class CategoryListModel
    {
        public int Count { get; set; }
        private ObservableCollection<string> _categories = new ObservableCollection<string>();

        public ObservableCollection<string> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }
    }
}
