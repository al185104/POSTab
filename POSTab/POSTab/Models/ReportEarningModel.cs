using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Models
{
    public class ReportEarningModel : BindableObject
    {

        [JsonProperty("EarningsDateList")]
        public ObservableCollection<EarningModel> EarningsDateList { get; set; } = new ObservableCollection<EarningModel>();

        [JsonProperty("Current")]
        public double Current { get; set; }

        [JsonProperty("Previous")]
        public double Previous { get; set; }
        public double GrandTotal { get; set; }

        public ReportEarningModel() { }
    }
}
