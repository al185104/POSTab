using POSTab.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace POSTab.Services.Dashboard
{
    interface IDashboardService
    {
        Task<ReportEarningModel> GetEarningsAsync(DateTime startDate, DateTime endDate);
        Task<ReportItemList> GetItemSoldList(DateTime startDate, DateTime endDate);
        Task<ReportCartList> GetCartList(DateTime startDate, DateTime endDate);
        Task<ReportStatistics> GetStoreStatistics();
    }
}
