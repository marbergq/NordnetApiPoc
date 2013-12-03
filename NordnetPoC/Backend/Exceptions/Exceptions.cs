using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.Backend.Exceptions
{
    [Serializable]
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
