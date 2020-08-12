using POSTab;
using POSTab.Helpers;
using POSTab.Models;
using POSTab.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTab.Services.Product
{
    public class ProductService : IProductService
    {
        #region Private Fields
        private readonly IRequestProvider _requestProvider;
        private const string ApiUrlBase = "api/Item";
        private const string ApiUrlItemCode = "/itemcode";
        private const string ApiUrlAll = "/all"; 
        #endregion
        
        #region Ctor
        public ProductService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        #endregion

        #region Properties
        private ApplicationProductList _applicationProductList;
        private ObservableCollection<ProductModel> _featuredProductList = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> _bestSellersList = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> _onSaleList = new ObservableCollection<ProductModel>();

        #region Categories
        private ObservableCollection<ProductModel> _beverages = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> _cannedGoods = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> _cleaners = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> _dryGoods = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> _paperGoods = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> _personalCare = new ObservableCollection<ProductModel>();
        private ObservableCollection<ProductModel> _others = new ObservableCollection<ProductModel>();

        public ObservableCollection<ProductModel> Others
        {
            get { return _others; }
            set { _others = value; }
        }

        public ObservableCollection<ProductModel> PersonalCare
        {
            get { return _personalCare; }
            set { _personalCare = value; }
        }

        public ObservableCollection<ProductModel> PaperGoods
        {
            get { return _paperGoods; }
            set { _paperGoods = value; }
        }

        public ObservableCollection<ProductModel> DryGoods
        {
            get { return _dryGoods; }
            set { _dryGoods = value; }
        }

        public ObservableCollection<ProductModel> Cleaners
        {
            get { return _cleaners; }
            set { _cleaners = value; }
        }

        public ObservableCollection<ProductModel> CannedGoods
        {
            get { return _cannedGoods; }
            set { _cannedGoods = value; }
        }

        public ObservableCollection<ProductModel> Beverages
        {
            get { return _beverages; }
            set { _beverages = value; }
        }
        #endregion

        public ObservableCollection<ProductModel> OnSaleList
        {
            get { return _onSaleList; }
            set { _onSaleList = value; }
        }

        public ObservableCollection<ProductModel> BestSellersList
        {
            get { return _bestSellersList; }
            set { _bestSellersList = value; }
        }

        public ObservableCollection<ProductModel> FeaturedProductList
        {
            get { return _featuredProductList; }
            set { _featuredProductList = value; }
        }

        public ApplicationProductList ApplicationProductList
        {
            get { return _applicationProductList; }
            set { _applicationProductList = value; }
        }
        #endregion

        #region Public Methods
        public async Task<ProductModel> PostProductAsync(ProductModel product, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, ApiUrlBase);

            ProductModel result;
            try
            {
                result = await _requestProvider.PostAsync(uri, product, token);
            }
            //If the status of the order has changed before to click cancel button, we will get a BadRequest HttpStatus
            catch (HttpRequestExceptionEx ex) when (ex.HttpCode == System.Net.HttpStatusCode.BadRequest)
            {
                result = null;
            }
            catch(HttpRequestExceptionEx ex)
            {
                if (ex.Message.Equals("\"Failed to add. Barcode already exists\""))
                    result = new ProductModel();
                else
                    result = null;
            }

            return result;
        }
        public async Task<bool> PutProductAsync(ProductModel product, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, ApiUrlBase);

            try
            {
                await _requestProvider.PutAsync(uri, product, token);
            }
            //If the status of the order has changed before to click cancel button, we will get a BadRequest HttpStatus
            catch (HttpRequestExceptionEx ex) when (ex.HttpCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }

            return true;
        }
        public async Task<ProductModel> GetProductByIDAsync(string guid, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{guid}");

            ProductModel product;

            try
            {
                product = await _requestProvider.GetAsync<ProductModel>(uri, token);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                product = null;
            }
            return product;
        }
        public async Task<ProductModel> GetProductByBarcodeAsync(string barcode, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlAll}/{barcode}");

            ProductModel product;

            try
            {
                product = await _requestProvider.GetAsync<ProductModel>(uri, token);
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                product = null;
            }
            return product;
        }

        public async Task<ApplicationProductList> GetAllProductsAsync()
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/all");

            ApplicationProductList result = await _requestProvider.GetAsync<ApplicationProductList>(uri);
            _applicationProductList = result;

            if (Beverages != null)
            {
                Beverages.Clear();
                CannedGoods.Clear();
                Cleaners.Clear();
                DryGoods.Clear();
                PaperGoods.Clear();
                PersonalCare.Clear();
                Others.Clear();
            }

            // populate product types
            foreach(var i in result.Items)
            {
                if (i.Type == null)
                    i.Type = "others";

                switch (i.Type.ToLower())
                {
                    case "beverage":
                        Beverages.Add(new ProductModel(i));
                        break;
                    case "canned goods":
                        CannedGoods.Add(new ProductModel(i));
                        break;
                    case "cleaners":
                        Cleaners.Add(new ProductModel(i));
                        break;
                    case "dry goods":
                        DryGoods.Add(new ProductModel(i));
                        break;
                    case "paper goods":
                        PaperGoods.Add(new ProductModel(i));
                        break;
                    case "personal care":
                        PersonalCare.Add(new ProductModel(i));
                        break;
                    default:
                        Others.Add(new ProductModel(i));
                        break;
                }
            }

            Beverages = SortCategory(_beverages);
            CannedGoods = SortCategory(_cannedGoods);
            Cleaners = SortCategory(_cleaners);
            DryGoods = SortCategory(_dryGoods);
            PaperGoods = SortCategory(_paperGoods);
            PersonalCare = SortCategory(_personalCare);
            Others = SortCategory(_others);
            
            return result;
        }

        private ObservableCollection<ProductModel> SortCategory(ObservableCollection<ProductModel> category)
        {
            ObservableCollection<ProductModel> list = new ObservableCollection<ProductModel>();
            var sorted = category.OrderBy(n => n.Name).ToList();
            foreach (var s in sorted)
                list.Add(s);
            return list;
        }

        public async Task<CategoryListModel> GetProductCategories()
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/all");

            CategoryListModel result = await _requestProvider.GetAsync<CategoryListModel>(uri);

            return result;
        }
        public async Task<bool> DeleteProductAsync(string guid, string token)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}{ApiUrlItemCode}/{guid}");
            try
            {
                await _requestProvider.DeleteAsync(uri, token);
                return true;
            }
            catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }

        public ProductModel FindItemByItemCode(string itemcode)
        {
            return _applicationProductList.Items.FirstOrDefault(i => i.ItemCode.Equals(itemcode));
        }
        #endregion
    }
}
