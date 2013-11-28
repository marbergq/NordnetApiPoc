using NordnetPoC.NordNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.NordNet
{
    public class BankProxy
    {
        /// <summary>
        /// Instancieates the login by the provided type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static LoginModel Porxy<T>(string username, string password) where T:new()
        {
            return Proxy<T>(username, password, null);
        }
        /// <summary>
        /// Instancieates the login by the provided type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static LoginModel Proxy<T>(string username, string password, string key) where T : new ()
        {
            LoginModel loginmodel;
            if(string.IsNullOrEmpty(key))
                loginmodel=(LoginModel)Activator.CreateInstance(typeof(T), new object[] { username, password});
            else
                loginmodel = (LoginModel)Activator.CreateInstance(typeof(T), new object[] { username, password,key});
            return loginmodel.PerformLogin();
        }
    }
}
