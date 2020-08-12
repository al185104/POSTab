using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Models
{
    public class ReportCartModel : BindableObject
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("SN")]
        public long SN { get; set; }

        [JsonProperty("CartKey")]
        public string CartKey { get; set; }

        [JsonProperty("CustomerID")]
        public long CustomerId { get; set; }

        [JsonProperty("Total")]
        public double Total { get; set; }

        [JsonProperty("TotalTax")]
        public double TotalTax { get; set; }

        [JsonProperty("Change")]
        public double Change { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("CreatedBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("Tender")]
        public double Tender { get; set; }

        [JsonProperty("TenderType")]
        public string TenderType { get; set; }

        [JsonProperty("UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

        [JsonProperty("UpdatedBy")]
        public long UpdatedBy { get; set; }
    }
}
