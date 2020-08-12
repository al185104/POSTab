using Microcharts;
using POSTab.Models;
using POSTab.Services.Dashboard;
using POSTab.ViewModels.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace POSTab.ViewModels.PageViews
{
    class DashboardViewModel : ViewModelBase
    {
        private DateTime today = DateTime.Now;
        private IDashboardService _dashboardService;
        private DateTime startDate = new DateTime();
        private DateTime endDate = DateTime.Now;
        private Chart transactions;
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; RaisePropertyChanged(() => IsRefreshing); }
        }

        public Chart Transactions
        {
            get { return transactions; }
            set { transactions = value; RaisePropertyChanged(() => Transactions); }
        }

        #region Constructor
        public DashboardViewModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
            startDate = new DateTime(today.Year, today.Month - 1, 1);
            _ = InitializeAsync(null);
        }
        #endregion
        public async override Task InitializeAsync(object navigationData)
        {
            ReportEarningModel dashboard = await _dashboardService.GetEarningsAsync(startDate, endDate);
            //GrandTotal = dashboard.GrandTotal.ToString("C2", CultureInfo.CreateSpecificCulture("en-PH"));
            List<ChartEntry> entries = new List<ChartEntry>();
            foreach (var dashboardEntry in dashboard.EarningsDateList)
            {
                var dotColor = "#57e695";
                if (dashboardEntry.TotalEarnings < 5000)
                    dotColor = "#FF2351";

                entries.Add(new ChartEntry(Convert.ToSingle(dashboardEntry.TotalEarnings))
                {
                    Label = dashboardEntry.Date.Day.ToString() + "/" + dashboardEntry.Date.Month.ToString(),
                    ValueLabel = dashboardEntry.TotalEarnings.ToString("C2", CultureInfo.CreateSpecificCulture("en-PH")),
                    TextColor =SKColors.Black,
                    Color = SKColor.Parse(dotColor)
                });
            }


            Transactions = new LineChart()
            {
                Entries = entries,
                LabelOrientation = Orientation.Horizontal,
                LabelColor = SKColors.Black,
                ValueLabelOrientation = Orientation.Horizontal,
                BackgroundColor = SKColors.Transparent
            };
        }

        public ICommand RefreshCommand => new Command( async () => await ExecuteRefreshCommand());
        private async Task ExecuteRefreshCommand()
        {
            IsRefreshing = true;

            await InitializeAsync(null);

            IsRefreshing = false;
        }
    }
}
