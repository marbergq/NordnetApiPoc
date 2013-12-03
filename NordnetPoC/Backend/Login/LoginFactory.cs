using NordnetPoC.BackEnd;
using NordnetPoC.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Backend.Login
{
    public class LoginFactory : ILoginFactory
    {
       
        public ICustomer CreateLogin(string username, string password, string key,LoginProviders bank)
        {
            switch (bank)
            {
                case LoginProviders.NordnetDirekt:
                    return new NordnetPoC.Banks.NordNet.NordNetModels.NordNetCustomer(BankProxy.Proxy<NordnetPoC.Banks.NordNet.NordNetLogin>(username,password,key));
                //more to come
            }
            throw new Exception();
        }

        public ICustomer CreateLogin(string username, string password, LoginProviders bank)
        {
            return CreateLogin(username, password, null, bank);
        }




        public ICustomer CreateLogin(Models.ILoginViewModel model)
        {
            return CreateLogin(model.UserName, model.Password,model.Key, model.Bank);
        }
    }
   
}
