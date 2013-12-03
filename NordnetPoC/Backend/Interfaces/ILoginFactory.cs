using NordnetPoC.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Backend.Interfaces
{
    public interface ILoginFactory
    {
        ICustomer CreateLogin(ILoginViewModel model);
        ICustomer CreateLogin(string username, string password, string key,LoginProviders provider); 
    }
    public enum LoginProviders { NordnetDirekt }
}
