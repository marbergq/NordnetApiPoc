using NordnetPoC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NordnetPoC.NordNet.Models
{
    class NordNetCustomer : AbstractCustomer
    { 
        
        public NordNetCustomer(LoginModel AuthedCustomer) : base(AuthedCustomer){}

        public override void ParseStocks(string PageToStockSource)
        {
            PageToStockSource = PageToStockSource.Substring(PageToStockSource.IndexOf("<caption class=\"bar\">Aktier</caption>"));
            var rows = PageToStockSource.Split(new[] { "<tr" }, StringSplitOptions.RemoveEmptyEntries).Where(s => Regex.IsMatch(s, "(class=\"even\")|(class=\"odd\")"));
            Stocks = rows.Select(s => new Aktie(s));
        }

        public override void setTodayChange(string page)
        {
            TodaysChange = Regex.Match(page, "(postrend[^>]+>(?<val>.*?)<)|(negtrend[^>]+>(?<val>.*?)<)", RegexOptions.Multiline).Groups["val"].Value;
        }

        public override void SetAccountBalance(string PageToAccountInfo)
        {
            var match = Regex.Match(PageToAccountInfo, "left\">\\s*<table[^>]+>\\s*<caption[^>]+>([^<]+)</caption>\\s*<tr[^>]+>\\s*<td[^>]+>[^<]+</td>\\s*<td>([^<]+)</td>\\s*</tr>\\s*<tr[^>]+>\\s*<td[^>]+>[^<]+</td>\\s*<td>([^<]+)</td>");
            Credits = match.Groups[2].Value;
            InvestedCapital = match.Groups[3].Value;
        }
     
    }

    class Aktie : IStock
    {


        public override string ToString()
        {
            return
                "Aktie: " + (InfoLink.Any()?InfoLink.FirstOrDefault().value:"") + "\n" +
                "Valuta: " + Currency + "\n" +
                "Antal :" + Count + "\n" +
                "Senast :" + LatestPrice + "\n" +
                "Dagens ändring :" + ToDayChange + "\n" +
                "AnskaffningsKurs (länk + värde) :" +
              (EnteryValue.Any()
              ? 
              EnteryValue.Select(s => "\t\n" + s.Url + " " + s.value + "\n").FirstOrDefault()
              :"")+ "\n" +
                "Belåningsvärde :" + PossibleDepthable + "\n" +
               "StartVärde :" + StartTotalValue + "\n" +
               "Marknadsvärde :" + CurrentMarketValue + "\n" +
               "Förändring (%) :" + ValueChangePercent + "\n" +
               "Förändring (SEK) :" + ValueChangeInCurrency;

        }


        public Aktie(string TRROW)
        {
            ParseTRrow(TRROW);
        }

        public override bool ParseTRrow(string row)
        {
            int i = 0;
            try{
         var matches= Regex.Matches(row,"<td.*?>(?<val>.*?)</td>");

         
            InfoLink = GetAktieLinks(matches[i++].Groups["val"].Value);
            Currency = matches[i++].Groups["val"].Value;
            Count = matches[i++].Groups["val"].Value;
            LatestPrice = matches[i++].Groups["val"].Value;
            ToDayChange = matches[i++].Groups["val"].Value;
            EnteryValue = GetAktieLinks(matches[i++].Groups["val"].Value);
            PossibleDepthable = matches[i++].Groups["val"].Value;
            StartTotalValue = matches[i++].Groups["val"].Value;
            CurrentMarketValue = matches[i++].Groups["val"].Value;
            ValueChangePercent = matches[i++].Groups["val"].Value;
            ValueChangeInCurrency = matches[i++].Groups["val"].Value;
            }catch(Exception e)
            {
            return false;
            }
            return true;

        
        
        }
        private IEnumerable<AktieLink> GetAktieLinks(string insideText)
        {
            foreach (Match match in Regex.Matches(insideText, "href=\"(?<link>.*?)\".*?>(?<value>.*?)</a>"))
            {
                yield return new AktieLink() { Url = match.Groups["link"].Value, value = match.Groups["value"].Value }; 
            }
        }


   
    }




    class AktieLink : IStockLink
    {

    }
}
