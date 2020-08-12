using POSTab.Helpers;
using POSTab.Models;
using POSTab.Services.RequestProvider;
using System.Threading.Tasks;

namespace POSTab.Services.Cart
{
    public class CartService : ICartService
    {

        private readonly IRequestProvider _requestProvider;
        private const string ApiUrlBase = "api/Cart";
        private const string ApiUrlAddCart = "add";
        private const string ApiUrlNewCart = "new?customerId=";
        private const string ApiUrlEndNewCart = "end";
        private const string ApiUrlPayCart = "pay";
        private const string ApiUrlUpdateCart = "update";
        
        private CartModel _cart = new CartModel();

        public CartModel Cart
        {
            get { return _cart; }
            set { _cart = value; }
        }


        public CartService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<CartModel> GetNewCartAsync(string guidUser, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{ApiUrlNewCart}{guidUser}");

            CartModel cart;
            try
            {
                cart = await _requestProvider.GetAsync<CartModel>(uri, token);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                cart = null;
            }

            return Cart = cart;
        }

        public async Task<bool> GetEndCartAsync(string cartId, bool abandoned, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{ApiUrlEndNewCart}/{cartId}?abandoned={abandoned}");
            try
            {
                await _requestProvider.GetAsync<CartModel>(uri, token);
                return true;
            }
            catch(HttpRequestExceptionEx ex)
            {
                return false;
            }
        }

        public async Task<CartModel> GetCartAsync(string cartId, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{cartId}");

            CartModel cart;

            try
            {
                cart = await _requestProvider.GetAsync<CartModel>(uri, token);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                cart = null;
            }
            return Cart = cart;
        }

        public async Task<CartModel> AddItemToCartAsync(int cartId, string token, string barcode, string qty = "1")
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{ApiUrlAddCart}/{barcode}?cartId={cartId.ToString()}&qty={qty}");

            CartModel cart;

            try
            {
                cart = await _requestProvider.GetAsync<CartModel>(uri, token);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                cart = null;
            }
            
            return Cart = cart;
        }

        public async Task<CartModel> GetCartPaymentAsync(string cartId, string token, int tenderType, double payment)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{ApiUrlPayCart}/{cartId}?tenderType={tenderType}&amountPaid={payment}");

            CartModel cart;

            try
            {
                cart = await _requestProvider.GetAsync<CartModel>(uri, token);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                cart = null;
            }

            return Cart = cart;
        }

        public async Task<CartModel> PostCartUpdateAsync(ProductModel product, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{ApiUrlUpdateCart}");

            CartModel cart;
            try
            {
                cart = await _requestProvider.PostAsync<CartModel>(uri, product, token);

            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                cart = null;
            }
            return Cart = cart;
        }

        public async Task<CartModel> DeleteCartItemAsync(string id, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}?transactionId={id}");

            CartModel cart;
            try
            {
                cart = await _requestProvider.DeleteAsync<CartModel>(uri, token);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                cart = null;
            }
            return Cart = cart;
        }
    }
}
