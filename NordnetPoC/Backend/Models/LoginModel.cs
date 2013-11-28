using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NordnetPoC.NordNet.Models
{
   public abstract class LoginModel
    {
       protected string username, password, key;
       public  LoginModel() { throw new FieldAccessException("Not allowed to use this"); }
     public  LoginModel(string username, string password, string key) { this.username = username; this.password = password; this.key = key; }
        public LoginModel(string username, string password){this.username=username;this.password=password;}
        public  HttpClient client{get;set;}
        public  string LoginPageResult{get;set;}
        public string UserID { get; set; }
        public string LoginURL { get; set; }

        public abstract LoginModel PerformLogin();
    }
}
