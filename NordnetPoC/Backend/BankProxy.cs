using NordnetPoC.BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.BackEnd
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
        public static LoginProvider Porxy<T>(string username, string password) where T:new()
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
        public static LoginProvider Proxy<T>(string username, string password, string key) where T : new ()
        {
            LoginProvider loginmodel;
            if(string.IsNullOrEmpty(key))
                loginmodel=(LoginProvider)Activator.CreateInstance(typeof(T), new object[] { username, password});
            else
                loginmodel = (LoginProvider)Activator.CreateInstance(typeof(T), new object[] { username, password,key});
            return loginmodel.PerformLogin();
        }
    }
}
