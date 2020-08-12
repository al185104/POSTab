using System.Threading.Tasks;

namespace POSTab.Services.Settings
{
    public interface ISettingsService
    {
        #region Default, may not be used.
        string AuthAccessToken { get; set; }
        string AuthIdToken { get; set; }
        bool UseMocks { get; set; }
        string IdentityEndpointBase { get; set; }
        string GatewayShoppingEndpointBase { get; set; }
        string GatewayMarketingEndpointBase { get; set; }
        bool UseFakeLocation { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
        bool AllowGpsLocation { get; set; }
        #endregion

        bool EnableAnimation { get; set; }
        bool AutoPrint { get; set; }
        bool DebugMode { get; set; }
        string DashboardEndDate { get; set; }
        string DashboardStartDate { get; set; }


        bool GetValueOrDefault(string key, bool defaultValue);
        string GetValueOrDefault(string key, string defaultValue);
        Task AddOrUpdateValue(string key, bool value);
        Task AddOrUpdateValue(string key, string value);
    }
}
