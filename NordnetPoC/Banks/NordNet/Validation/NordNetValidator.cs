using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Banks.NordNet.Validation
{
    class NordNetValidator : NordnetPoC.Backend.Validator.IValidator
    {

        public bool ValidateStillLoggedIn(string content)
        {
            return !content.Contains("logga in");
        }

        public bool ValidateLogin(string content)
        {
            return !content.Contains("fel vid inloggningen");
        }
    }
}
