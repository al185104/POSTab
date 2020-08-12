using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace POSTab.Models
{
    public class ProductModel : BindableObject
    {
        #region Properties
        [JsonProperty("transactionId")]
        public int TransactionId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public double TotalPrice { get; set; }

        [JsonProperty("itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("unitPrice")]
        public double UnitPrice { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        
        [JsonProperty("stockCount")]
        public int StockCount{ get; set; }

        [JsonProperty("isQuantityRequired")]
        public bool IsQuantityRequired { get; set; }

        [JsonProperty("isPriceRequired")]
        public bool IsPriceRequired { get; set; }

        
        private string image = "settings_productimage_placeholder.png";
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        public bool IsEditing { get; set; } = false;
        public string CardBGColor { get; set; }
        public int EntryId { get; set; }
        #endregion

        #region Constructor
        public ProductModel(ProductModel obj)
        {
            TransactionId = obj.TransactionId;
            Type = obj.Type;
            TotalPrice = obj.TotalPrice;
            ItemCode = obj.ItemCode;
            Name = obj.Name;
            Description = obj.Description;
            UnitPrice = obj.UnitPrice;
            Quantity = obj.Quantity;
            StockCount = obj.StockCount;
            IsQuantityRequired = obj.IsQuantityRequired;
            IsPriceRequired = obj.IsPriceRequired;
            Image = obj.Image;
            CreatedDate = obj.CreatedDate;
            IsEditing = obj.IsEditing;
            CardBGColor = obj.CardBGColor;
            EntryId = obj.EntryId;
        }
        public ProductModel() { } 
        #endregion
    }
}
