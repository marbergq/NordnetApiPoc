using NordnetPoC.Backend.Interfaces;
using NordnetPoC.Backend.Login;
using NordnetPoC.Banks.NordNet.NordNetModels;
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
        
        /// <summary>
        /// Just to prove the login and listing of your account, not to be used :)
        /// </summary>
        /// <param name="args"></param>
        /// 


        static void Main(string[] args)
        {

            LoginFactory f = new LoginFactory();
            ICustomer customer = f.CreateLogin("ID", "PWD", "KEY", LoginProviders.NordnetDirekt);

            foreach(var aktie in customer.Stocks)
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
            foreach (var aktie in customer.Stocks)
            {
                Console.WriteLine(aktie.ToString());
                Console.WriteLine("\n");

            }
            Console.ReadLine();
        }
    }
}
