using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NordNetApiPoC.NordNetAPI.DataContracts;
using NordNetApiPoC.NordNetAPI.AbstractRequests;
namespace NordNetApiPoC.NordNetAPI.StockcsModule
{
    class Instruments : AbstractRequestClass
    {
        /// <summary>
        /// Geven a stock, gets the instrument information
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        public Instrument GetInstrument(Stock stock)
        {
            var parameterz = new Dictionary<string, string>();
            parameterz["identifier"] = stock.Identifier;
            parameterz["marketID"] = stock.MarketID.ToString();

            return MakeRequest<Instrument>(HttpMethods.GET, "instruments", parameterz);
                
        }

        /// <summary>
        /// Same as GetInstrument, but with several stocks
        /// </summary>
        /// <param name="stocks"></param>
        /// <returns></returns>
        public IEnumerable<Instrument> GetMultipleInstruments(IEnumerable<Stock> stocks)
        {
            var parameterz = new Dictionary<string, string>();

            stocks.ToList().ForEach(
                s => parameterz.Add(s.MarketID.ToString(), s.Identifier)
                );
            return MakeRequest<IEnumerable<Instrument>>(HttpMethods.GET, "instruments", parameterz)??new List<Instrument>();
        }

    }

    class StockInfo : AbstractRequestClass
    {
        /// <summary>
        /// Gets All markets available on Nordnet
        /// </summary>
        public IEnumerable<Market> getAllMarkets
        {
            get
            {
                return
                    MakeRequest<IEnumerable<Market>>(HttpMethods.GET, "lists")??new List<Market>();
            }
        }

        /// <summary>
        /// Gets all stocks in all markets (Exhasting!)
        /// </summary>
        public IEnumerable<Stock> getAllStocks
        {
            get
            {
                foreach (var market in getAllMarkets)
                {
                    foreach (var stock in getAllStocksInMarket(market))
                        yield return stock;
                }
            }
        }

        /// <summary>
        /// Find all Stocks 
        /// </summary>
        /// <param name="market">Market to search in</param>
        /// <returns>All Stocks in that market</returns>
        public IEnumerable<Stock> getAllStocksInMarket(Market market)
        {
            return MakeRequest<IEnumerable<Stock>>(HttpMethods.GET, "lists", market.ID)??new List<Stock>();
        }

        /// <summary>
        /// Gets todays chartData given a stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns>Chart Data</returns>
        public IEnumerable<ChartData> getStockChartData(Stock stock)
        {
            var dictionary = new Dictionary<string, string>();
            dictionary["marketID"] = stock.MarketID.ToString();
            dictionary["identifier"] = stock.Identifier;
            try
            {
                return MakeRequest<IEnumerable<ChartData>>(HttpMethods.GET, "chart_data", dictionary)??new List<ChartData>();
            }
            catch (System.Web.HttpException)
            {
                return null;
            }
        }
    }
}
