using POSTab.Models;
using POSTab.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace POSTab.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected readonly INavigationService NavigationService;
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set 
            { 
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase()
        {
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public void ScanItem(string itemcode)
        {
            MessagingCenter.Send(this, MessageKeys.ScanItem, itemcode);
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("HH:mm:ss");
        }
    }
}
