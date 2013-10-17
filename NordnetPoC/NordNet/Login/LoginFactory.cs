using NordnetPoC.Interfaces;
using NordnetPoC.NordNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.NordNet.Login
{
    class LoginFactory : ILoginFactory
    {
        private static readonly string NordNetLoginUrl = "https://www.nordnetdirekt.se/mux/inloggad/lib/login.html";
        public ICustomer CreateLogin(string username, string password, string key,LoginProviders bank)
        {
            switch (bank)
            {
                case LoginProviders.NordnetDirekt:
                    return new NordNet.Models.NordNetCustomer(NordNetproxy(username, password, key));
                //more to come
            }
            throw new Exception();
        }
        private LoginModel NordNetproxy(string username, string password, string key)
        {
            var model = NordNetLogin(username, password, key);
            return model.Result;
        }
        private async Task<LoginModel> NordNetLogin(string username, string password, string key)
        {
            HttpClient client = new HttpClient();
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("a1", username));
            list.Add(new KeyValuePair<string, string>("a2", password));
            list.Add(new KeyValuePair<string, string>("usa", "7"));
            list.Add(new KeyValuePair<string, string>("a3", "ADSE"));
            list.Add(new KeyValuePair<string, string>("a4", "sv"));
            list.Add(new KeyValuePair<string, string>("nyckel", key));

            var respons = await client.PostAsync(NordNetLoginUrl, new FormUrlEncodedContent(list));
            respons.EnsureSuccessStatusCode();
            string responseContent = await respons.Content.ReadAsStringAsync();
            if (!responseContent.Contains("fel vid inloggningen"))
            {
                //success :)
                return new LoginModel { client = client, LoginPageResult = responseContent, UserID = username, LoginURL=respons.RequestMessage.RequestUri.ToString() };

            } 
            
            throw new LoginErrorException();
        }

    }
    public class LoginErrorException : Exception
    {

        public override string Message
        {
            get
            {
                return "Access denied with those Criedentals";
            }
        }
    }
}
