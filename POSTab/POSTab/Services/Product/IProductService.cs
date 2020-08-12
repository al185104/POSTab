using POSTab.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace POSTab.Services.Product
{
    public interface IProductService
    {
        Task<ProductModel> PostProductAsync(ProductModel product, string token);
        Task<bool> PutProductAsync(ProductModel product, string token);
        Task<ApplicationProductList> GetAllProductsAsync();
        Task<ProductModel> GetProductByIDAsync(string guid, string token);
        Task<ProductModel> GetProductByBarcodeAsync(string barcode, string token);
        Task<bool> DeleteProductAsync(string guid, string token);
        Task<CategoryListModel> GetProductCategories();
        ProductModel FindItemByItemCode(string itemcode);

        #region Properties
        ObservableCollection<ProductModel> FeaturedProductList { get; set; }
        ObservableCollection<ProductModel> BestSellersList { get; set; }
        ObservableCollection<ProductModel> OnSaleList { get; set; }
        ObservableCollection<ProductModel> Beverages { get; set; }
        ObservableCollection<ProductModel> CannedGoods { get; set; }
        ObservableCollection<ProductModel> Cleaners { get; set; }
        ObservableCollection<ProductModel> DryGoods { get; set; }
        ObservableCollection<ProductModel> PaperGoods { get; set; }
        ObservableCollection<ProductModel> PersonalCare { get; set; }
        ObservableCollection<ProductModel> Others { get; set; }
        #endregion
    }
}
