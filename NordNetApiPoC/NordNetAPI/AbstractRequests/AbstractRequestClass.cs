using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NordNetApiPoC.NordNetAPI.AbstractRequests
{
    public class NotInitalizedException : Exception
    {
        public NotInitalizedException() : base("Session key not set!") { }
    }

  

     public abstract class AbstractRequestClass 
    {

        public string SessionKey { protected get; set; }
        public DateTime LastRequestTime { get; protected set; }


        private void EnsureInit()
        {
            if (String.IsNullOrEmpty(SessionKey))
                throw new NotInitalizedException();
        }

        protected T MakeRequest<T>(string method, string action)
        {
            EnsureInit();
            var response = nExtRequestUtil.SendRequest(method, action, null, SessionKey);
            LastRequestTime = DateTime.Now;
            return JSONSerializer<T>.readResponse(response);
        }


        protected T MakeRequest<T>(string method, string action, Dictionary<string, string> requestItems)
        {
            EnsureInit();
            var response = nExtRequestUtil.SendRequest(method, action, "", requestItems, SessionKey);
            LastRequestTime = DateTime.Now;
            return JSONSerializer<T>.readResponse(response);
        }

        protected T MakeRequest<T>(string method, string controller, string action)
        {
            EnsureInit();
            var response = nExtRequestUtil.SendRequest(method, controller, action, null, SessionKey);
            LastRequestTime = DateTime.Now;
            return JSONSerializer<T>.readResponse(response);
        }
        public static T LoginRequest<T>(Dictionary<string, string> parameterz)
        {
            var response = nExtRequestUtil.SendRequest(HttpMethods.POST, "login", parameterz);
            return JSONSerializer<T>.readResponse(response);
        }

        static class JSONSerializer<T>
        {

            public static T readResponse(WebResponse response)
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(response.GetResponseStream());
            }
        }
    }
}
