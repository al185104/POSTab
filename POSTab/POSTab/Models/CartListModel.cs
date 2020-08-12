using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Models
{
    public class CartListModel : BindableObject
    {
        public string CartID { get; set; }
        public string TransactionDate { get; set; }
        public double Total { get; set; }
        public double Tender { get; set; }
        public double Change { get; set; }
    }
}
