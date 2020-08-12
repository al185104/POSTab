using POSTab.Services;
using POSTab.Services.Cart;
using POSTab.Services.Dashboard;
using POSTab.Services.Navigation;
using POSTab.Services.Product;
using POSTab.Services.RequestProvider;
using POSTab.Services.Settings;
using POSTab.ViewModels;
using POSTab.ViewModels.PageViews;
using POSTab.ViewModels.Popup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using TinyIoC;
using Xamarin.Forms;

namespace POSTab.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static TinyIoCContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            _container.Register<MainPageViewModel>().AsSingleton();

            // view controls
            _container.Register<HomeViewModel>().AsSingleton();

            // pop up
            _container.Register<ReceiptPopupPageViewModel>().AsSingleton();
            //_container.Register<PayPopupViewModel>();
            //_container.Register<ReceiptPopupPageViewModel>();
            //_container.Register<EnterBarcodePopupViewModel>();
            //_container.Register<ProcessingCartPopupViewModel>();

            // Services - by default, TinyIoC will register interface registrations as singletons.
            _container.Register<INavigationService, NavigationService>().AsSingleton();
            _container.Register<IRequestProvider, RequestProvider>().AsSingleton();
            _container.Register<ICartService, CartService>().AsSingleton();
            _container.Register<IProductService, ProductService>().AsSingleton();
            _container.Register<IDashboardService, DashboardService>().AsSingleton();
            _container.Register<ISettingsService, SettingsService>().AsSingleton();
        }

        public static void UpdateDependencies(bool useMockServices)
        {
            // Change injected dependencies
            if (useMockServices)
            {
                // add mock services here. Just to see your code is running.
                UseMockService = true;
            }
            else
            {
                // add actual services here.
                UseMockService = false;
            }
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
