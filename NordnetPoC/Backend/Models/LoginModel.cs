using NordnetPoC.Backend.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.BackEnd.Models
{
    public abstract class LoginProvider
    {
        protected string username, password, key;
        
        public LoginProvider() { throw new FieldAccessException("Not allowed to use this"); }
        
        public LoginProvider(string username, string password, string key, IValidator Validator) { this.username = username; this.password = password; this.key = key; this.Validator = Validator; }
        
        public LoginProvider(string username, string password, IValidator Validator) { this.username = username; this.password = password; this.Validator = Validator; }
        
        private IValidator Validator;
        
        private HttpClient _client;
        
        public HttpClient Client
        {
            get
            {
                if (_client == null)
                    _client = new HttpClient();
                return _client;
            }
        }
        
        public string LoginPageResult { get; set; }

        public string UserID { get; set; }
        
        public string LoginURL { get; set; }
        public string LoginOKUrl { get; set; }
        private IEnumerable<KeyValuePair<string, string>> _loginParamters { get; set; }
        protected IEnumerable<KeyValuePair<string, string>> LoginParamters { set { _loginParamters = value; } }
        
        public LoginProvider PerformLogin()
        {
           _loginParamters= GenerateLoginParamters();
            if (_loginParamters == null || _loginParamters.Count() == 0)
                throw new ArgumentNullException("LoginParamters");
            if (string.IsNullOrEmpty(LoginURL))
                throw new ArgumentNullException("LoginUrl");

            var respons = Client.PostAsync(LoginURL, new FormUrlEncodedContent(_loginParamters)).Result;
            respons.EnsureSuccessStatusCode();
            string responseContent = respons.Content.ReadAsStringAsync().Result;

            if (!Validator.ValidateLogin(responseContent))
            {
                throw new NordnetPoC.NordNet.Login.LoginErrorException();
            }

            LoginPageResult = responseContent;
            LoginOKUrl = respons.RequestMessage.RequestUri.ToString();
            UserID = username;
            return this;
        }

        protected abstract IEnumerable<KeyValuePair<string, string>> GenerateLoginParamters();
    }
}
