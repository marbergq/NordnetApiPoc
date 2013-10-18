
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using NordNetApiPoC.NordNetAPI.StockcsModule;
using NordNetApiPoC.NordNetAPI.News;
//using NordNetApiPoC.NordNetAPI.Feed;

namespace nExt_login
{
    enum ACTION 
    { 
        LISTSTOCKS = 0,
        LIST_MARKETS = 1, 
        LIST_NEWS = 2
    }
    class Program
    {
        static NordNetApiPoC.NordNetAPI.LoginModule.Login login;
        /// <summary>
        /// Just a console printout to prove the case, not to be used
        /// </summary>
        /// <param name="arg"></param>
        public static void Main(string[] arg)
        {
            if (login == null)
            {
                login = new NordNetApiPoC.NordNetAPI.LoginModule.Login();

                login.UserName = "marbergq";
                login.PassWord = "skateboard1";

                var result = login.PreformLogin();
                //if logged in
                if (result)
                {


                    //Non working feed, for hte momente :)
                    //using (var socketfeed = new StreamReader(FeedHandler.ConnectPrivateFeed(login)))
                    //{

                    //    for (int i = 0; i < 10; i++)
                    //    {
                    //        var feedLoginResult = socketfeed.ReadLine();
                    //        Console.WriteLine(feedLoginResult);
                    //    }
                    //}
                    if(!string.IsNullOrEmpty(arg[0]))
                    switch (int.Parse(arg[0]))
                    {
                        case (int)ACTION.LIST_MARKETS:
                            PrintAllStocksInfo(login);
                            break;
                        case (int)ACTION.LIST_NEWS:
                            PrintNewsSources(login);
                            break;
                        case (int)ACTION.LISTSTOCKS: 
                            PrintNews(login);
                            break;
                    }


                    //Stocks
                    //if (result)
                    //{

                    //        Console.WriteLine(" ");
                    Console.WriteLine("Yey!");

                }
            }
           Main( new[]{Console.ReadLine()});
        
        }

        private static void PrintAllStocksInfo(NordNetApiPoC.NordNetAPI.LoginModule.Login login)
        {
            foreach (var market in login.Stockinfo.getAllMarkets)
            {
                Console.WriteLine(market.Country);
                Console.WriteLine(market.ID);
                Console.WriteLine(market.Name);
                Console.WriteLine(market.SortOrder);
                Console.WriteLine("STOCKS:");
                foreach (var stock in login.Stockinfo.getAllStocksInMarket(market))
                {
                    Console.WriteLine(" ");
                    Console.WriteLine(stock.ShortName);
                    Console.WriteLine(stock.Identifier);
                    try
                    {
                        foreach (var chartD in login.Stockinfo.getStockChartData(stock))
                        {
                            Console.WriteLine("");
                            Console.WriteLine(chartD.Change);
                            Console.WriteLine(chartD.Price);
                            Console.WriteLine(chartD.TimeStamp);

                            Console.WriteLine("");

                        }
                    }
                    catch { }

                    //Exhausting
                    //var instrument = login.InstrumentsInfo.GetInstrument(stock);

                    //Console.WriteLine(" ");
                    //Console.WriteLine(instrument.Country);
                    //Console.WriteLine(instrument.Currency);
                    //Console.WriteLine(instrument.Identifier);
                    //Console.WriteLine(instrument.IsinCode);
                    //Console.WriteLine(instrument.LongName);
                    //Console.WriteLine(instrument.MarketID);
                    //Console.WriteLine(instrument.ShortName);
                    //Console.WriteLine(instrument.Type);





                    //Doesn't work in test
                    //var stockdata = StockInfo.getStockChartData(login, stock);
                    //if (stockdata != null)
                    //    foreach (var tick in stockdata)
                    //    {
                    //        Console.WriteLine(" ");
                    //        Console.WriteLine(tick.Change);
                    //        Console.WriteLine(tick.Price);
                    //        Console.WriteLine(tick.TimeStamp);
                    //        Console.WriteLine(tick.Volume);
                    //    }

                }
            }
        }

        private static void PrintNewsSources(NordNetApiPoC.NordNetAPI.LoginModule.Login login)
        {
            try
            {
                foreach (var news in login.Newsinfo.getAllNewsSources)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine(news.Name);
                    Console.WriteLine(news.Code);
                    Console.WriteLine(news.Level);
                    Console.WriteLine(news.Sourceid);
                    Console.WriteLine(news.imageurl);
                    Console.WriteLine("Countries: ");
                    foreach (var country in news.Countries)
                        Console.WriteLine(country.CountryCode);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("did not find any news");
            }
        }

        private static void PrintNews(NordNetApiPoC.NordNetAPI.LoginModule.Login login)
        {
            try
            {
                foreach (var newsItem in login.Newsinfo.getNewsItems(2))
                {
                    Console.WriteLine(" ");
                    Console.WriteLine(newsItem.Datetime);
                    Console.WriteLine(newsItem.HeadLine);
                    Console.WriteLine(newsItem.ItemID);
                    Console.WriteLine(newsItem.SourceId);
                    Console.WriteLine(newsItem.type);

                }
            }
            catch (Exception)
            {
                Console.WriteLine("did not find any news");
            }
        }

    }
}