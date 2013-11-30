using NordnetPoC.Backend.Constants;
using NordnetPoC.BackEnd.Models;
using NordnetPoC.Banks.NordNet.Validation;
using NordnetPoC.NordNet.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Banks.NordNet
{
    public class NordNetLogin : LoginProvider
    {
        /// <summary>
        /// ForBidden
        /// </summary>
        public NordNetLogin() : base(){}
        public NordNetLogin(string username, string password, string key) : base(username, password, key, new NordNetValidator()) { LoginURL = URLS.NordNetLoginUrl; }



        protected override IEnumerable<KeyValuePair<string, string>> GenerateLoginParamters()
        {
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("a1", username));
            list.Add(new KeyValuePair<string, string>("a2", password));
            list.Add(new KeyValuePair<string, string>("usa", "7"));
            list.Add(new KeyValuePair<string, string>("a3", "ADSE"));
            list.Add(new KeyValuePair<string, string>("a4", "sv"));
            list.Add(new KeyValuePair<string, string>("nyckel", key));
            return list;
        }

    }
}
