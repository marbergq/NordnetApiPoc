using NordnetPoC.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Backend.Models
{
    public class ILoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// Ok to be null if not applicable for bank
        /// </summary>
        public string Key { get; set; }
        public LoginProviders Bank { get; set; }

    }
}
