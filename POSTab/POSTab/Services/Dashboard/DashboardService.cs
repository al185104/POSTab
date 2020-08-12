using POSTab.Helpers;
using POSTab.Models;
using POSTab.Services.RequestProvider;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POSTab.Services.Dashboard
{
    class DashboardService : IDashboardService
    {
        private readonly IRequestProvider _requestProvider;
        private const string ApiUrlBase = "api/Report";
        private const string ApiUrlEarnings = "/earnings";
        private const string ApiUrlItemsSold = "/soldItems";
        private const string ApiUrlCartSold = "/carts";
        private const string ApiUrlStoreStats = "/storeStatistics";

        #region Constructor
        public DashboardService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }
        #endregion

        public async Task<ReportItemList> GetItemSoldList(DateTime startDate, DateTime endDate)
        {
            string start = startDate.ToString("yyyy-MM-dd");
            string end = endDate.ToString("yyyy-MM-dd");
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{ApiUrlItemsSold}?startDate={start}&endDate={end}&pageSize=1000&currentPage=1");

            try
            {
                return await _requestProvider.GetAsync<ReportItemList>(uri);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
            }

            return new ReportItemList();
        }

        public async Task<ReportEarningModel> GetEarningsAsync(DateTime startDate, DateTime endDate)
        {
            string start = startDate.ToString("yyyy-MM-dd");
            string end = endDate.ToString("yyyy-MM-dd");

            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{ApiUrlEarnings}?startDate={start}&endDate={end}");

            try
            {
                return await _requestProvider.GetAsync<ReportEarningModel>(uri);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
            }

            return new ReportEarningModel();
        }

        public async Task<ReportCartList> GetCartList(DateTime startDate, DateTime endDate)
        {
            string start = startDate.ToString("yyyy-MM-dd");
            string end = endDate.ToString("yyyy-MM-dd");

            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{ApiUrlCartSold}?startDate={start}&endDate={end}&pageSize=1000&currentPage=1");

            try
            {
                return await _requestProvider.GetAsync<ReportCartList>(uri);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
            }

            return new ReportCartList();
        }

        public async Task<ReportStatistics> GetStoreStatistics()
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.GatewayShoppingEndpoint, $"{ApiUrlBase}/{ApiUrlStoreStats}");

            try
            {
                return await _requestProvider.GetAsync<ReportStatistics>(uri);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + e.Message);
            }

            return new ReportStatistics();
        }
    }
}
