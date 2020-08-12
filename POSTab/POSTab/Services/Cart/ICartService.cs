using POSTab.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POSTab.Services.Cart
{
    public interface ICartService
    {
        Task<CartModel> GetNewCartAsync(string guidUser, string token);
        Task<bool> GetEndCartAsync(string cartId, bool abandon, string token);
        Task<CartModel> AddItemToCartAsync(int cartId, string token, string barcode, string qty = "1");
        Task<CartModel> GetCartAsync(string cartId, string token);
        Task<CartModel> GetCartPaymentAsync(string cartId, string token, int tenderType, double payment);
        Task<CartModel> PostCartUpdateAsync(ProductModel product, string token);
        Task<CartModel> DeleteCartItemAsync(string id, string token);
        CartModel Cart { get; set; }
    }
}
