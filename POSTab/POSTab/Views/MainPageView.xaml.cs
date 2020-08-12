using POSTab.ViewModels;
using POSTab.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace POSTab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : ContentPage
    {
        List<ContentView> views = new List<ContentView>();
        int selectedView = 0;

        public MainPageView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            views.Add(Home);
            views.Add(Dashboard);
            views.Add(Messages);
            views.Add(Bills);
            views.Add(Settings);

            MessagingCenter.Unsubscribe<MainPageViewModel, int>(this, MessageKeys.SelectOption);
            MessagingCenter.Subscribe<MainPageViewModel, int>(this, MessageKeys.SelectOption, async (sender, arg) =>
            {
                await views[selectedView].FadeTo(0);
                views[selectedView].IsVisible = false;
                views[arg].IsVisible = true;
                _ = views[arg].FadeTo(1);

                selectedView = arg;
            });
        }

        public void Scanner(string keys, string v)
        {
            // temp item
            // keys = "4800066122212";
            MessagingCenter.Send(this, MessageKeys.ScanItem, keys);
        }
    }
}