using NordnetPoC.NordNet.Models;
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
        IEnumerable<IStock> Stocks { get; set; }
    }

    interface ICustomer
    {

        IEnumerable<IStock> GetStocks();
        string Credits { get; set; }
        string InvestedCapital { get; set; }

        /// <summary>
        /// Requries to have been logged in successfully
        /// </summary>

        void ParseStocks(string rowForStock);
        void setClient(HttpClient authedClient);
        void setTodayChange(string Page);
        string TodaysChange { get; }
        void SetAccountBalance(string PageToAccountInfo);
        void updateData();
    }

    abstract class AbstractCustomer : ICustomer
    {
        /// <summary>
        /// The actually client which was used when logged in.
        /// </summary>
        protected HttpClient client;
        
        public readonly string LoginID;
        
        /// <summary>
        /// All stocks owned
        /// </summary>
        public IEnumerable<IStock> Stocks { get; protected set; }
        /// <summary>
        /// The page where the users stocks are listed. 
        /// </summary>
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

        /// <summary>
        /// Refresh data, throws LoginErrorException if not logged in
        /// </summary>
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

        /// <summary>
        /// Provide the client used for the login. if the client allready exists, throws argumentException
        /// </summary>
        /// <param name="authedClient"></param>
        public void setClient(HttpClient authedClient)
        {
            if (client == null)
                client = authedClient;
            else
                throw new ArgumentException("Client allready set");
        }

        /// <summary>
        /// All owned stocks
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IStock> GetStocks()
        {
            return Stocks;
        }

        private string _Credits;

        /// <summary>
        /// Total amount available
        /// </summary>
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
        /// <summary>
        /// Login provided
        /// </summary>
        private LoginModel loginModel;
        /// <summary>
        /// The capital invested
        /// </summary>
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
        public abstract bool ParseTRrow(string row);
    }

    /// <summary>
    /// Links to more stock info
    /// </summary>
    abstract class IStockLink
    {
        public string Url { get; set; }
        public string value { get; set; }
    }
}
