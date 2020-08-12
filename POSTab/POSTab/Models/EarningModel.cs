using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Models
{
    public class EarningModel : BindableObject
    {

        [JsonProperty("Column1")]
        public DateTime Date { get; set; }

        [JsonProperty("TotalEarnings")]
        public double TotalEarnings { get; set; }
    }
}
