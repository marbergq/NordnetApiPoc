using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NordNetApiPoC.NordNetAPI.LoginModule ;

namespace NordNetApiPoc
{
    [TestClass]
    public class RequestClassesTest
    {
        Login login = null;
        [TestInitialize]
        public void Init()
        {
            login = new Login { UserName = "marbergq", PassWord = "skateboard1" };
            Assert.IsTrue(login.PreformLogin());
        }

        /// <summary>
        /// Will fail
        /// </summary>
        [TestMethod]
        public void TestAllNews()
        {
            //Will Fail in test
            var AllNews = login.Newsinfo.getAllNewsSources;
            Assert.IsNotNull(AllNews);
        }

        /// <summary>
        /// Will fail
        /// </summary>
        [TestMethod]
        public void Test10News()
        {
            //Will Fail
            var tenNews = login.Newsinfo.getNewsItems(10);
            Assert.IsNotNull(tenNews);
        
        }

        [TestMethod]
        public void TestGettingAllMarkets()
        {
            var allStocks = login.Stockinfo.getAllMarkets;
            Assert.IsNotNull(allStocks);
        }

        [TestMethod]
        public void TestGettAllStocksInAMarket()
        {
            var getAllMarkets = login.Stockinfo.getAllMarkets;
            foreach(var first in getAllMarkets)
            {
                var stocks = login.Stockinfo.getAllStocksInMarket(first);
                Assert.IsNotNull(stocks);
            }
        }

    }
}
