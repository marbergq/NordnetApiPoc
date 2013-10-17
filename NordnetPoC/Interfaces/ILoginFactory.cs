using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Interfaces
{
    interface ILoginFactory
    {
        ICustomer CreateLogin(string username, string password, string key,LoginProviders provider); 
    }
    enum LoginProviders { NordnetDirekt }
}
