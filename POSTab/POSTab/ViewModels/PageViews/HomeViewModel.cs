using POSTab.Helpers;
using POSTab.Models;
using POSTab.Services.Navigation;
using POSTab.Services.Product;
using POSTab.ViewModels.Base;
using POSTab.ViewModels.Popup;
using POSTab.Views;
using POSTab.Views.PageViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace POSTab.ViewModels.PageViews
{
    class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<StickerModel> categorySticker = new ObservableCollection<StickerModel>();
        private StickerModel selectedCategory;
        private CartModel ereceipt = new CartModel();
        private IProductService _productService;
        private INavigationService _navigationService;
        private int entryId = 0;

        public ProductModel SelectedItem { get; set; }
        public ProductModel SelectedReceiptItem { get; set; }

        public CartModel EReceipt
        {
            get { return ereceipt; }
            set { ereceipt = value; RaisePropertyChanged(() => EReceipt); }
        }

        public StickerModel SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (selectedCategory != value && value != null)
                {
                    selectedCategory = value;
                    RaisePropertyChanged(() => SelectedCategory);
                    int index = categorySticker.IndexOf(selectedCategory);
                    MessagingCenter.Send(this, MessageKeys.SelectCategory, index);
                }
            }
        }

        #region Item Category Lists
        private ObservableCollection<ProductModel> beveragesList;
        private ObservableCollection<ProductModel> cannedGoodsList;
        private ObservableCollection<ProductModel> cleanersList;
        private ObservableCollection<ProductModel> dryGoodsList;
        private ObservableCollection<ProductModel> othersList;
        private ObservableCollection<ProductModel> paperGoodsList;
        private ObservableCollection<ProductModel> personalCareList;

        public ObservableCollection<ProductModel> BeveragesList
        {
            get { return beveragesList; }
            set { beveragesList = value; RaisePropertyChanged(() => BeveragesList); }
        }
        public ObservableCollection<ProductModel> CannedGoodsList
        {
            get { return cannedGoodsList; }
            set { cannedGoodsList = value; RaisePropertyChanged(() => CannedGoodsList); }
        }
        public ObservableCollection<ProductModel> CleanersList
        {
            get { return cleanersList; }
            set { cleanersList = value; RaisePropertyChanged(() => CleanersList); }
        }
        public ObservableCollection<ProductModel> DryGoodsList
        {
            get { return dryGoodsList; }
            set { dryGoodsList = value; RaisePropertyChanged(() => DryGoodsList); }
        }
        public ObservableCollection<ProductModel> OthersList
        {
            get { return othersList; }
            set { othersList = value; RaisePropertyChanged(() => OthersList); }
        }
        public ObservableCollection<ProductModel> PaperGoodsList
        {
            get { return paperGoodsList; }
            set { paperGoodsList = value; RaisePropertyChanged(() => PaperGoodsList); }
        }
        public ObservableCollection<ProductModel> PersonalCareList
        {
            get { return personalCareList; }
            set { personalCareList = value; RaisePropertyChanged(() => PersonalCareList); }
        } 
        #endregion

        public ObservableCollection<StickerModel> CategorySticker
        {
            get { return categorySticker; }
            set { categorySticker = value; RaisePropertyChanged(() => CategorySticker); }
        }

        #region Constructor
        public HomeViewModel(IProductService productService, INavigationService navigationService)
        {
            _productService = productService;
            _navigationService = navigationService;

            SetCategoryStickers();
            SelectedCategory = categorySticker[0];

            MessagingCenter.Unsubscribe<MainPageView, string>(this, MessageKeys.ScanItem);
            MessagingCenter.Subscribe<MainPageView, string>(this, MessageKeys.ScanItem, (sender, arg) =>
            {
                ExecuteAddItemCommand(_productService.FindItemByItemCode(arg));
            });

            MessagingCenter.Unsubscribe<ReceiptPopupPageViewModel>(this, MessageKeys.NewCart);
            MessagingCenter.Subscribe<ReceiptPopupPageViewModel>(this, MessageKeys.NewCart, (sender) =>
            {
                entryId = 0;
                EReceipt = new CartModel();
            });

            _ = LoadItems();
        } 
        #endregion

        private async Task LoadItems()
        {
            await GetAllItems();

            if (BeveragesList == null)
                BeveragesList = _productService.Beverages;
            if (CannedGoodsList == null)
                CannedGoodsList = _productService.CannedGoods;
            if (CleanersList == null)
                CleanersList = _productService.Cleaners;
            if (DryGoodsList == null)
                DryGoodsList = _productService.DryGoods;
            if (OthersList == null)
                OthersList = _productService.Others;
            if (PaperGoodsList == null)
                PaperGoodsList = _productService.PaperGoods;
            if (PersonalCareList == null)
                PersonalCareList = _productService.PersonalCare;
        }

        private async Task GetAllItems()
        {
            var productService = ViewModelLocator.Resolve<IProductService>();
            await productService.GetAllProductsAsync();
        }

        private void SetCategoryStickers()
        {
            CategorySticker.Add(new StickerModel() { StringLogo = IconFont.Fire, Label = "Hot" });
            CategorySticker.Add(new StickerModel() { StringLogo = IconFont.Hamburger, Label = "Burger" });
            CategorySticker.Add(new StickerModel() { StringLogo = IconFont.Pizza, Label = "Pizza" });
            CategorySticker.Add(new StickerModel() { StringLogo = IconFont.FoodForkDrink, Label = "Snack" });
            CategorySticker.Add(new StickerModel() { StringLogo = IconFont.BottleSodaOutline, Label = "Soft Drink" });
            CategorySticker.Add(new StickerModel() { StringLogo = IconFont.CoffeeOutline, Label = "Coffee" });
            CategorySticker.Add(new StickerModel() { StringLogo = IconFont.IceCream, Label = "Ice Cream" });
        }

        public ICommand AddItemCommand => new Command((obj) => ExecuteAddItemCommand(obj));
        private void ExecuteAddItemCommand(object obj)
        {
            SelectedItem = obj as ProductModel;
            ProductModel p = new ProductModel();
            p = SelectedItem;
            p.Quantity = 1;
            p.EntryId = entryId;
            p.TotalPrice = p.Quantity * p.UnitPrice;
            EReceipt.Items.Insert(0, new ProductModel(p));
            EReceipt.Total = EReceipt.Items.Sum(s => s.UnitPrice * s.Quantity);
            entryId++;
            RaisePropertyChanged(() => EReceipt);
            SelectedItem = ereceipt.Items[0];
        }

        public ICommand ChargeCommand => new Command(async () => await ExecuteChargeCommand());

        private async Task ExecuteChargeCommand()
        {
            ereceipt.Change = 0;
            ereceipt.Tender = 0;
            await _navigationService.NavigateToPopUpAsync<ReceiptPopupPageViewModel>(ereceipt);
        }

        public ICommand EditCommand => new Command((obj) => ExecuteEditCommand(obj));

        private void ExecuteEditCommand(object obj)
        {
            var o = obj;
        }

        public ICommand DeleteCommand => new Command((obj) => ExecuteDeleteCommand(obj));

        private void ExecuteDeleteCommand(object obj)
        {
            ProductModel o = obj as ProductModel;
            ereceipt.Items.Remove(ereceipt.Items.Where(i => i.EntryId == o.EntryId).Single());
            EReceipt.Total = EReceipt.Items.Sum(s => s.UnitPrice * s.Quantity);
            RaisePropertyChanged(() => EReceipt);
        }
    }
}
