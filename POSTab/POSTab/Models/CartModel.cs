using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Models
{
    public class CartModel : BindableObject
    {
        public int Id { get; set; }
        public string CartKey { get; set; }
        public int CustomerId { get; set; }
        public double Total { get; set; }
        public double TotalTax { get; set; }
        public double Change { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public ObservableCollection<ProductModel> Items { get; set; } = new ObservableCollection<ProductModel>();
        public string TenderType { get; set; }
        public double Tender { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
