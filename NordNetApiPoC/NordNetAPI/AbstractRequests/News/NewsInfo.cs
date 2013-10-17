using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NordNetApiPoC.NordNetAPI.LoginModule;
using NordNetApiPoC.NordNetAPI.DataContracts;
using NordNetApiPoC.NordNetAPI.AbstractRequests;
namespace NordNetApiPoC.NordNetAPI.News
{
    class NewsInfo : AbstractRequestClass
    {

        /// <summary>
        /// Gets all newsitems sa user have rights to see, might be null
        /// </summary>
        /// <param name="loginInformation"></param>
        /// <returns></returns>
        public IEnumerable<NewsSource> getAllNewsSources
        {
            get
            {
                return MakeRequest<IEnumerable<NewsSource>>(HttpMethods.GET, "news_sources") ?? new List<NewsSource>() ;
            }
        }

        /// <summary>
        /// Gets News
        /// </summary>
        /// <param name="count">number of news to list</param>
        /// <returns></returns>
        public IEnumerable<NewsItem> getNewsItems(int count)
        {
            var requestItems = new Dictionary<string, string>();
            requestItems["count"] = count.ToString();
            return MakeRequest<IEnumerable<NewsItem>>(HttpMethods.GET, "news_items", requestItems)??new List<NewsItem>();
        }
    }
}
