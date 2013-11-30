using NordnetPoC.Backend.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Interfaces
{
    interface IStockPage
    {
        string Lista { get; set; }
        HttpClient Client { get; set; }
        IEnumerable<AbstractStock> Stocks { get; set; }
    }

    public interface IStockLink
    {
        string URL { get; }
        string Value { get; }
    }

   public interface IStock
    {
       IEnumerable<IStockLink> InfoLink { get; }
        string Currency { get; }
        string Count { get;  }
        string LatestPrice { get; }
        string ToDayChange { get; }
        IEnumerable<IStockLink> EnteryValue { get; }
        string PossibleDepthable { get; }
        string StartTotalValue { get; }
        string CurrentMarketValue { get; }
        string ValueChangePercent { get; }
        string ValueChangeInCurrency { get; }
    }

    interface ICustomer
    {
        DateTime LastUpdate { get; }
        IEnumerable<IStock> GetStocks();
        string Credits { get; set; }
        string InvestedCapital { get; set; }

        /// <summary>
        /// Requries to have been logged in successfully
        /// </summary>

        string TodaysChange { get; }
        void updateData();
    }

  


}
