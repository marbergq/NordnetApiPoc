using NordnetPoC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NordnetPoC.Backend.Abstracts;
using NordnetPoC.BackEnd.Models;
using NordnetPoC.Banks.NordNet.Validation;

namespace NordnetPoC.Banks.NordNet.NordNetModels
{
   public class NordNetCustomer : AbstractCustomer
    { 
        
        public NordNetCustomer(LoginProvider AuthedCustomer) : base(AuthedCustomer,new NordNetValidator()){}

       
        public override IEnumerable<AbstractStock> ParseStocks(string PageToStockSource)
        {
            PageToStockSource = PageToStockSource.Substring(PageToStockSource.IndexOf("<caption class=\"bar\">Aktier</caption>"));
            var rows = PageToStockSource.Split(new[] { "<tr" }, StringSplitOptions.RemoveEmptyEntries).Where(s => Regex.IsMatch(s, "(class=\"even\")|(class=\"odd\")"));
            return rows.Select(s => new NordNetStock(s));
        }

        public override string ParseTodayChange(string page)
        {
            var match = Regex.Match(page, "(postrend[^>]+>(?<val>.*?)<)|(negtrend[^>]+>(?<val>.*?)<)", RegexOptions.Multiline).Groups["val"].Value;
            return string.IsNullOrEmpty(match)?"0.0":match;
        }

        public override Dictionary<string,string> ParseAccountBalance(string PageToAccountInfo)
        {
            Dictionary<string,string> hits = new Dictionary<string,string>();
            var match = Regex.Match(PageToAccountInfo, "left\">\\s*<table[^>]+>\\s*<caption[^>]+>([^<]+)</caption>\\s*<tr[^>]+>\\s*<td[^>]+>[^<]+</td>\\s*<td>([^<]+)</td>\\s*</tr>\\s*<tr[^>]+>\\s*<td[^>]+>[^<]+</td>\\s*<td>([^<]+)</td>");
            hits["credits"] = match.Groups[2].Value; 
            hits["invested"]=match.Groups[3].Value;
            return hits;
        }
     
    }

    public class NordNetStock : AbstractStock
    {
        public NordNetStock(string stockRowToParse) : base(stockRowToParse) { }
        public override string ToString()
        {
            return
                "Aktie: " + (_infoLink.Any()?_infoLink.First().Value:"") + "\n" +
                "Valuta: " + Currency + "\n" +
                "Antal :" + Count + "\n" +
                "Senast :" + LatestPrice + "\n" +
                "Dagens ändring :" + ToDayChange + "\n" +
                "AnskaffningsKurs (länk + värde) :" +
              (EnteryValue.Any()
              ? 
              EnteryValue.Select(s => "\t\n" + s.URL + " " + s.Value + "\n").FirstOrDefault()
              :"")+ "\n" +
                "Belåningsvärde :" + PossibleDepthable + "\n" +
               "StartVärde :" + StartTotalValue + "\n" +
               "Marknadsvärde :" + CurrentMarketValue + "\n" +
               "Förändring (%) :" + ValueChangePercent + "\n" +
               "Förändring (SEK) :" + ValueChangeInCurrency;

        }


        /// <summary>
        /// Parses a NordNetstock
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public override bool ParseStock(string row)
        {
            int i = 0;
            try{
         var matches= Regex.Matches(row,"<td.*?>(?<val>.*?)</td>");

         
            _infoLink = ParseStockLinks(matches[i++].Groups["val"].Value);
            Currency = matches[i++].Groups["val"].Value;
            Count = matches[i++].Groups["val"].Value;
            LatestPrice = matches[i++].Groups["val"].Value;
            ToDayChange = matches[i++].Groups["val"].Value;
            EnteryValue = ParseStockLinks(matches[i++].Groups["val"].Value);
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
        private IEnumerable<StockLink> ParseStockLinks(string insideText)
        {
            foreach (Match match in Regex.Matches(insideText, "href=\"(?<link>.*?)\".*?>(?<value>.*?)</a>"))
            {
                yield return new StockLink(match.Groups["link"].Value,match.Groups["value"].Value); 
            }
        }


   
    }
}
