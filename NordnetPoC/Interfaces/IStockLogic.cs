using NordnetPoC.NordNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Interfaces
{
    interface ICustomer
    {
      
        IEnumerable<IStock> GetStocks();
        string Credits{get;set;}
        string InvestedCapital { get; set; }
        /// <summary>
        /// Requries to have been logged in successfully
        /// </summary>

       void ParseStocks(string rowForStock);
        void setClient(HttpClient authedClient);
        void setTodayChange(string Page );
        string TodaysChange { get; }
        void SetAccountBalance(string PageToAccountInfo);
         void updateData();
    }
    abstract class AbstractCustomer : ICustomer
    {
        protected HttpClient client;
        public readonly string LoginID;
        public IEnumerable<IStock> Stocks { get; protected set; }
        private string pageForInfo { get; set; }

        public string TodaysChange { get; protected set; }
        public abstract void ParseStocks(string rowForStock);
        public abstract void SetAccountBalance(string PageToAccountInfo);
        public abstract void setTodayChange(string Page);


        public AbstractCustomer(LoginModel AuthedCustomer)
        {
            pageForInfo = AuthedCustomer.LoginURL;
            setClient(AuthedCustomer.client);
            LoginID = AuthedCustomer.UserID;
            setTodayChange(AuthedCustomer.LoginPageResult);
            SetAccountBalance(AuthedCustomer.LoginPageResult);
            ParseStocks(AuthedCustomer.LoginPageResult);
        }

         public async void updateData()
        {
            var respons = await client.GetAsync(pageForInfo);
            
            var html = await respons.Content.ReadAsStringAsync();

            if (html.Contains("logga in"))
                throw new NordnetPoC.NordNet.Login.LoginErrorException();
            setTodayChange(html);
            SetAccountBalance(html);
            ParseStocks(html);
        }

        public void setClient(HttpClient authedClient)
        {
            if (client == null)
                client = authedClient;
            else
                throw new Exception();
        }

        public IEnumerable<IStock> GetStocks()
        {
            return Stocks;
        }

        private string _Credits;
        public string Credits
        {
            get
            {
                return _Credits;
            }
            set
            {
                _Credits = value;
            }
        }
        private string _InvestedCapital;
        private LoginModel loginModel;
        public string InvestedCapital
        {
            get
            {
                return _InvestedCapital;
            }
            set
            {
                _InvestedCapital = value;
            }
        }
    }

    abstract class IStock
    {
        public IEnumerable<IStockLink> InfoLink { get; set; }
        public string Currency { get; set; }
        public string Count { get; set; }
        public string LatestPrice { get; set; }
        private string _TodayChange;
        public string ToDayChange { 
            get {
                return string.IsNullOrEmpty(_TodayChange) ? "" : _TodayChange;
        }
            set {
                _TodayChange = value;
            }
        }
        public IEnumerable<IStockLink> EnteryValue { get; set; }
        public string PossibleDepthable { get; set; }
        public string StartTotalValue { get; set; }
        public string CurrentMarketValue { get; set; }
        public string ValueChangePercent { get; set; }
        public string ValueChangeInCurrency { get; set; }
        public abstract bool ParseTRrow(string row);
    }
    abstract class IStockLink
    {
        public string Url { get; set; }
        public string value { get; set; }
    }
}
