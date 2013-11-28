using NordnetPoC.Backend.Constants;
using NordnetPoC.NordNet.Login;
using NordnetPoC.NordNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Banks.NordNet
{
    public class NordNetLogin : LoginModel
    {
        public NordNetLogin() : base(){}
       public NordNetLogin(string username, string password, string key) : base(username, password, key) { }

        
        public override LoginModel PerformLogin()
        {
            LoginURL = URLS.NordNetLoginUrl;
            client = new HttpClient();
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("a1", username));
            list.Add(new KeyValuePair<string, string>("a2", password));
            list.Add(new KeyValuePair<string, string>("usa", "7"));
            list.Add(new KeyValuePair<string, string>("a3", "ADSE"));
            list.Add(new KeyValuePair<string, string>("a4", "sv"));
            list.Add(new KeyValuePair<string, string>("nyckel", key));

            var respons = client.PostAsync(LoginURL, new FormUrlEncodedContent(list)).Result;
            respons.EnsureSuccessStatusCode();
            string responseContent = respons.Content.ReadAsStringAsync().Result;
            if (!responseContent.Contains("fel vid inloggningen"))
            {
                LoginPageResult = responseContent;
                UserID = username;
                //success :)
                //return new LoginModel { client = client, LoginPageResult = responseContent, UserID = username, LoginURL = respons.RequestMessage.RequestUri.ToString() };
                return this;
            }

            throw new LoginErrorException();
        }
    }
}
