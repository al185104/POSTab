using POSTab.Helpers;
using POSTab.Models;
using POSTab.Services.Product;
using POSTab.ViewModels.Base;
using POSTab.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace POSTab.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<StickerModel> optionSticker = new ObservableCollection<StickerModel>();
        private StickerModel selectedOption;

        public StickerModel SelectedOption
        {
            get { return selectedOption; }
            set 
            {
                if (selectedOption != value && value != null)
                {
                    selectedOption = value;
                    RaisePropertyChanged(() => SelectedOption);
                    int index = optionSticker.IndexOf(selectedOption);
                    MessagingCenter.Send(this, MessageKeys.SelectOption, index);
                }
            }
        }

        public ObservableCollection<StickerModel> OptionSticker
        {
            get { return optionSticker; }
            set { optionSticker = value; }
        }


        #region Constructor
        public MainPageViewModel()
        {
            SetOptionStickers();
        } 
        #endregion

        private void SetOptionStickers()
        {
            OptionSticker.Add(new StickerModel() { StringLogo = IconFont.HomeOutline, Label = "Home" });
            OptionSticker.Add(new StickerModel() { StringLogo = IconFont.ViewDashboardOutline, Label = "Dashboard" });
            OptionSticker.Add(new StickerModel() { StringLogo = IconFont.MessageTextOutline, Label = "Messages" });
            OptionSticker.Add(new StickerModel() { StringLogo = IconFont.Receipt, Label = "Bills" });
            OptionSticker.Add(new StickerModel() { StringLogo = IconFont.Cog, Label = "Setting" });
            SelectedOption = optionSticker[0];
        }
    }
}
