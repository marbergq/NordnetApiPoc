using NordnetPoC.Backend.Exceptions;
using NordnetPoC.Backend.Validator;
using NordnetPoC.BackEnd.Models;
using NordnetPoC.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Backend.Abstracts
{

    public abstract class AbstractCustomerInfoParser
    {
        public string TodaysChange { get; protected set; }
        protected string _InvestedCapital;
        protected string _Credits;
        /// <summary>
        /// All stocks owned
        /// </summary>
        public IEnumerable<AbstractStock> _stocks { get; protected set; }
        public abstract IEnumerable<AbstractStock> ParseStocks(string rowForStock);
        public abstract Dictionary<string,string> ParseAccountBalance(string PageToAccountInfo);
        public abstract string ParseTodayChange(string Page);

        /// <summary>
        /// Parse method that is running upon sucessfull login.
        /// Excepects result from ParseAccountBalance to hold "invested" and "Capital"
        /// </summary>
        /// <param name="page"></param>
        protected void ParseData(string page)
        {
            _stocks=ParseStocks(page);
          TodaysChange=  ParseTodayChange(page);
          var accountbalance = ParseAccountBalance(page);
          _InvestedCapital = accountbalance["invested"];
          _Credits = accountbalance["credits"];
        }
    }

    public abstract class AbstractCustomer : AbstractCustomerInfoParser, ICustomer
    {
        public AbstractCustomer(LoginProvider AuthedCustomer,IValidator validator)
        {
            //Inject validator
            this.Validator = validator;
            pageForInfo = AuthedCustomer.LoginOKUrl;
            setClient(AuthedCustomer.Client);
            LoginID = AuthedCustomer.UserID;
            ParseData(AuthedCustomer.LoginPageResult);
            
        }

        private IValidator Validator;
        
        /// <summary>
        /// The actually client which was used when logged in.
        /// </summary>
        protected HttpClient client;

        DateTime _lastUpdate;

        public readonly string LoginID;

        
        /// <summary>
        /// The page where the users stocks are listed. 
        /// </summary>
        private string pageForInfo { get; set; }


        /// <summary>
        /// Refresh data, throws LoginErrorException if not logged in
        /// </summary>
        public async void updateData()
        {
            var respons = await client.GetAsync(pageForInfo);

            var html = await respons.Content.ReadAsStringAsync();

            if (!Validator.ValidateStillLoggedIn(html))
                throw new LoginErrorException();
            ParseData(html);
            _lastUpdate = DateTime.Now;
        }

        


        /// <summary>
        /// Provide the client used for the login. if the client allready exists, does nothing
        /// </summary>
        /// <param name="authedClient"></param>
        public void setClient(HttpClient authedClient)
        {
            if (client == null)
                client = authedClient;
        }



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

        /// <summary>
        /// All owned stocks
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStock> ICustomer.Stocks
        {
            get
            {
                return _stocks;
            }
        }

        public DateTime LastUpdate
        {
            get { return _lastUpdate; }
        }
    }
}
