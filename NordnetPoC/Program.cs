using NordnetPoC.Interfaces;
using NordnetPoC.NordNet.Login;
using NordnetPoC.NordNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NordnetPoC
{
    class Program
    {
        private static readonly string LoginUrl = "https://www.nordnetdirekt.se/mux/inloggad/lib/login.html";
        
        /// <summary>
        /// Just to prove the login and listing of your account, not to be used :)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            LoginFactory f = new LoginFactory();
           ICustomer customer =  f.CreateLogin("ID", "PW", "KEY", LoginProviders.NordnetDirekt);

            foreach(var aktie in customer.GetStocks())
            {
                Console.WriteLine(aktie.ToString());
                Console.WriteLine("\n");
            
            }

            Console.WriteLine("Depåutveckling idag: " + customer.TodaysChange);
            Console.WriteLine("Värdepappersaldo: " + customer.InvestedCapital);
            Console.WriteLine("Likvida medel: " + customer.Credits);


            Console.ReadLine();
            Console.WriteLine("Updating...");
            customer.updateData();
            foreach (var aktie in customer.GetStocks())
            {
                Console.WriteLine(aktie.ToString());
                Console.WriteLine("\n");

            }
            Console.ReadLine();



        }

        private async static void MakeRequests(HttpClient client)
        { 
            var respons = await client.PostAsync(LoginUrl,new FormUrlEncodedContent(getLoginData("671895","Mse6kf","199004175056")));
            respons.EnsureSuccessStatusCode();
            string responseContent = await respons.Content.ReadAsStringAsync();
            if (responseContent.Contains("7658974"))
                Console.WriteLine("Success");
            else
                Console.WriteLine("Fail");

            //Console.WriteLine(responseContent);
            FindStocks(responseContent);
        }

        private static IEnumerable<Aktie> FindStocks(string startPage)
        {
            startPage = startPage.Substring(startPage.IndexOf("<caption class=\"bar\">Aktier</caption>"));
            var rows = startPage.Split(new []{"<tr"},StringSplitOptions.RemoveEmptyEntries).Where(s=>Regex.IsMatch(s,"(class=\"even\")|(class=\"odd\")"));
                //Regex.Split(startPage, "class.*?/tr>", RegexOptions.Multiline);
                //.Where(s=>Regex.IsMatch(s,"(class=\"even\")|(class=\"odd\")"));
            var aktier = rows.Select(s => new Aktie(s));

           foreach(var aktie in aktier)
               Console.WriteLine(aktie.ToString());
           return aktier;
        }

        private static List<KeyValuePair<string, string>> getLoginData(string username, string password, string key)
        { 
            var list = new List<KeyValuePair<string,string>>();
            list.Add(new KeyValuePair<string,string>("a1",username));
            list.Add(new KeyValuePair<string,string>("a2",password));
            list.Add(new KeyValuePair<string,string>("usa","7"));
            list.Add(new KeyValuePair<string,string>("a3","ADSE"));
            list.Add(new KeyValuePair<string,string>("a4","sv"));
            list.Add(new KeyValuePair<string,string>("nyckel",key));
            return list;
        }
    }
}
