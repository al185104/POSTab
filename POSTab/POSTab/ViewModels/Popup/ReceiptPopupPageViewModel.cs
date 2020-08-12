using POSTab.Models;
using POSTab.Services.Cart;
using POSTab.Services.Navigation;
using POSTab.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace POSTab.ViewModels.Popup
{
    class ReceiptPopupPageViewModel : ViewModelBase
    {
        private CartModel cart = new CartModel();
        private double tenderInput = 0;
        private string chargeButtonColor = "#B2B1B6";
        private bool chargeButtonIsEnabled = false;
        private INavigationService _navigationService;
        private ICartService _cartService;

        public bool ChargeButtonIsEnabled
        {
            get { return chargeButtonIsEnabled; }
            set { chargeButtonIsEnabled = value; RaisePropertyChanged(() => ChargeButtonIsEnabled); }
        }

        public string ChargeButtonColor
        {
            get { return chargeButtonColor; }
            set { chargeButtonColor = value; RaisePropertyChanged(() => ChargeButtonColor); }
        }

        public double TenderInput
        {
            get { return tenderInput; }
            set { tenderInput = value; RaisePropertyChanged(() => TenderInput); }
        }

        public CartModel Cart
        {
            get { return cart; }
            set { cart = value; RaisePropertyChanged(() => Cart); }
        }

        #region Constructor
        public ReceiptPopupPageViewModel(INavigationService navigationService, ICartService cartService)
        {
            _navigationService = navigationService;
            _cartService = cartService;
        } 
        #endregion

        public override Task InitializeAsync(object navigationData)
        {
            if(navigationData != null && navigationData is CartModel)
            {
                TenderInput = 0;
                ChargeButtonIsEnabled = false;
                ChargeButtonColor = "#B2B1B6";
                Cart = navigationData as CartModel;
            }
            return base.InitializeAsync(navigationData);
        }

        public ICommand KeyInCommand => new Command((obj) => ExecuteKeyInCommand(obj));

        private void ExecuteKeyInCommand(object obj)
        {
            int x = 0;
            if (obj != null && obj is string)
            {
                string input = obj as string;
                switch (input)
                {
                    case "C":
                        TenderInput = 0;
                        break;
                    case "+":
                        TenderInput = Cart.Total;
                        break;
                    case "00":
                        TenderInput *= 100;
                        break;
                    case "100":
                        TenderInput = 100;
                        break;
                    case "500":
                        TenderInput = 500;
                        break;
                    case "1000":
                        TenderInput = 1000;
                        break;
                    default:
                        tenderInput = tenderInput * 10;
                        double i = Convert.ToDouble(input) / 100;
                        TenderInput = tenderInput + i;
                        break;
                };

                var state = tenderInput >= Cart.Total ? "Valid" : "Invalid";
                if(tenderInput >= Cart.Total)
                {
                    ChargeButtonIsEnabled = true;
                    ChargeButtonColor = "#FF2351";

                    Cart.Tender = tenderInput;
                    Cart.Change = Cart.Tender - Cart.Total;
                }
                else
                {
                    ChargeButtonIsEnabled = false;
                    ChargeButtonColor = "#B2B1B6";
                    
                    Cart.Tender = 0;
                    Cart.Change = 0;
                }
                RaisePropertyChanged(() => Cart);
            }
        }

        public ICommand ChargeCommand => new Command(async () => await ExecuteChargeCommand());

        private async Task ExecuteChargeCommand()
        {
            IsBusy = true;
            // get new cart
            CartModel newCart = await _cartService.GetNewCartAsync("0", "temp_token");
            // iterate add for each item.
            foreach (var item in cart.Items)
            {
                var cc = await _cartService.AddItemToCartAsync(newCart.Id, "temp_token", item.ItemCode, item.Quantity.ToString());
                if (cc == null)
                    throw new Exception("Something went wrong in processing cart!");
            }
            // pay for cart
            _ = await _cartService.GetCartPaymentAsync(newCart.Id.ToString(), "temp_token", 700, cart.Tender);
            MessagingCenter.Send(this, MessageKeys.NewCart);
            // end transaction
            await _cartService.GetEndCartAsync(newCart.Id.ToString(), false, "temp_token");
            // delete cart
            cart = new CartModel();
            IsBusy = false;
            await _navigationService.RemoveLastFromBackStackAsync(true);
        }
    }
}
