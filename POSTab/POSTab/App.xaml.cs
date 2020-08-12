using POSTab.Services.Navigation;
using POSTab.Services.Product;
using POSTab.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace POSTab
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjkzOTIxQDMxMzgyZTMyMmUzMGtINFgweUIrSHVwTmlqTFAzZ1N3Yzk3V3BtVmd5SFFNaWxZUm1DZzhaWmc9");
        }

        protected async override void OnStart()
        {
            base.OnStart();
            await InitNavigation();
            base.OnResume();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }
    }
}
