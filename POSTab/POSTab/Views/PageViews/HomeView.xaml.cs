using POSTab.ViewModels.Base;
using POSTab.ViewModels.PageViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace POSTab.Views.PageViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentView
    {
        List<VisualElement> categoryContents = new List<VisualElement>();
        int selectionIndex = 0;
        public HomeView()
        {
            InitializeComponent();

            categoryContents.Add(BeveragesControl);
            categoryContents.Add(CannedGoodsControl);
            categoryContents.Add(CleanersControl);
            categoryContents.Add(DryGoodsControl);
            categoryContents.Add(PaperGoodsControl);
            categoryContents.Add(PersonalCareControl);
            categoryContents.Add(OthersControl);

            MessagingCenter.Unsubscribe<HomeViewModel, int>(this, MessageKeys.SelectCategory);
            MessagingCenter.Subscribe<HomeViewModel, int>(this, MessageKeys.SelectCategory, async (sender, arg) =>
            {
                if (arg != -1)
                    {
                    await categoryContents[selectionIndex].FadeTo(0);
                    categoryContents[selectionIndex].IsVisible = false;
                    categoryContents[arg].IsVisible = true;
                    _ = categoryContents[arg].FadeTo(1);

                    selectionIndex = arg;
                }
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, MessageKeys.ItemAdded);
        }

    }
}