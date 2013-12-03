using NordnetPoC.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Backend.Abstracts
{
    public abstract class AbstractStock : IStock
    {
        public AbstractStock(string stockInfoToParse)
        {
            ParseStock(stockInfoToParse);
        }

        IEnumerable<IStockLink> InfoLink { get; set; }
        public string Currency { get; set; }
        public string Count { get; set; }
        public string LatestPrice { get; set; }
        private string _TodayChange;

        /// <summary>
        /// Actually change today
        /// </summary>
        public string ToDayChange
        {
            get
            {
                return string.IsNullOrEmpty(_TodayChange) ? "" : _TodayChange;
            }
            set
            {
                _TodayChange = value;
            }
        }
        public IEnumerable<IStockLink> EnteryValue { get; set; }
        public string PossibleDepthable { get; set; }
        public string StartTotalValue { get; set; }
        public string CurrentMarketValue { get; set; }
        public string ValueChangePercent { get; set; }
        public string ValueChangeInCurrency { get; set; }
        public abstract bool ParseStock(string row);

        protected IEnumerable<IStockLink> _infoLink;
        IEnumerable<IStockLink> IStock.InfoLink
        {
            get
            {
                return _infoLink;
            }
        }

        protected IEnumerable<IStockLink> _entryValue;
        IEnumerable<IStockLink> IStock.EnteryValue
        {
            get { return _entryValue; }
        }
    }

    /// <summary>
    /// Links to more stock info
    /// </summary>
    public class StockLink : IStockLink
    {
        public StockLink(string url, string value)
        {
            _url = url;
            _value = value;
        }

        private string _url;
        public string URL
        {
            get { return _url; }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
        }
    }
}
