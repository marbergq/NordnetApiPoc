using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.NordNet.Models
{
    class LoginModel
    {
      public  HttpClient client{get;set;}
      public  string LoginPageResult{get;set;}
        public string UserID { get; set; }
        public string LoginURL { get; set; }
    }
}
