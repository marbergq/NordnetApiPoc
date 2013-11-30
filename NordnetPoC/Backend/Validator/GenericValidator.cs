using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Backend.Validator
{
   public interface IValidator
    {
        bool ValidateLogin(string content);
        bool ValidateStillLoggedIn(string content);
    }
}
