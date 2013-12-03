using NordnetPoC.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialStockMarket.Models.Bank
{
    public class BankLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// Ok to be null if not applicable for bank
        /// </summary>
        public string Key { get; set; }
        public LoginProviders Bank { get; set; }
        public IEnumerable<SelectListItem> Banks
        {
            get
            {
               return Enum.GetItems<LoginProviders>().Select(s => new SelectListItem() { Text=s.ToString(),Value=s.ToString()});
            }
        }
    }
    public class BankLoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// Ok to be null if not applicable for bank
        /// </summary>
        public string Key { get; set; }
        public string Bank { get; set; }
        public LoginProviders selectedBank { 
            get 
        {
            return Enum.GetItems<LoginProviders>().Where(s => s.ToString() == Bank).Single();
        
        }
        
        }
    }

    public static class Enum
    {
        public static IEnumerable<T> GetItems<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}