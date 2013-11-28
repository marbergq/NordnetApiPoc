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
       
        public ICustomer CreateLogin(string username, string password, string key,LoginProviders bank)
        {
            switch (bank)
            {
                case LoginProviders.NordnetDirekt:
                    return new NordNet.Models.NordNetCustomer(BankProxy.Proxy<NordnetPoC.Banks.NordNet.NordNetLogin>(username,password,key));
                //more to come
            }
            throw new Exception();
        }

        public ICustomer CreateLogin(string username, string password, LoginProviders bank)
        {
            return CreateLogin(username, password, null, bank);
        }

        //private LoginModel NordNetproxy(string username, string password, string key)
        //{

        //}

        //private async Task<LoginModel> NordNetLogin(string username, string password, string key)
        //{
            
        //}

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
