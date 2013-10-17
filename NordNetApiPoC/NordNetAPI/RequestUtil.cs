using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace NordNetApiPoC.NordNetAPI
{
    class RequestUtil
    {
        public static string BASE_URL = "https://api.test.nordnet.se/next";
        public static string VERSION = "1";
    }
    class HttpMethods
    {
        public const string POST = "POST";
        public const string GET = "GET";
        public const string PUT = "PUT";
        public const string DELETE = "DELETE";
    }
    class nExtRequestUtil
    {
        public static T Deserialize<T>(string xml)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
            {

                T reply = Activator.CreateInstance<T>();

                XmlSerializer serializer = new XmlSerializer(reply.GetType());

                reply = (T)serializer.Deserialize(ms);

                return reply;
            }
        }
        private static string CreateParameterList(Dictionary<string, string> parameters)
        {
            // Create parameter list
            StringBuilder data = new StringBuilder();
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    data.Append(data.Length == 0 ? "" : "&");
                    data.Append(parameter.Key + "=" +
            HttpUtility.UrlEncode(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(parameter.Value)));
                }
            }
            return data.ToString(); ;
        }

        public static WebResponse SendRequest(string method, string requestPath, string actionParameter, Dictionary<string, string> parameters, string auth)
        {
            if (parameters == null)
                return SendRequest(method, requestPath + "/" + actionParameter, parameters, auth);
            //make method that does requestpath ? paramters.each( key=value& )
            else if (parameters != null && string.IsNullOrEmpty(actionParameter))
                return SendRequest(method, requestPath + "?" + CreateParameterList(parameters), parameters, auth);

            else
                throw new NotImplementedException();
        }

        public static WebResponse SendRequest(string method, string requestPath, Dictionary<string, string> parameters, string auth)
        {

            try
            {
                // Should ONLY be used in test where the certificate is not real.
                ServicePointManager.ServerCertificateValidationCallback +=
                     delegate(
                         object sender,
                         X509Certificate certificate,
                         X509Chain chain,
                         SslPolicyErrors sslPolicyErrors)
                     {
                         return true;
                     };

                // Create the URL
                Uri address = new Uri(RequestUtil.BASE_URL + "/" + RequestUtil.VERSION + "/" + requestPath);

                // Create request
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

                request.Method = method;
                request.Accept = "application/json";
                request.Headers["Authorization"] = "Basic " + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(auth));
                request.ContentType = "application/x-www-form-urlencoded";

                if (parameters != null && (method.Equals(HttpMethods.POST) || method.Equals(HttpMethods.PUT) || method.Equals(HttpMethods.DELETE)))
                {
                    request.ContentType = "application/x-www-form-urlencoded";

                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(nExtRequestUtil.CreateParameterList(parameters));

                    request.ContentLength = byteData.Length;

                    // Write data  
                    using (Stream postStream = request.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                    }
                }


                return request.GetResponse();
                // Get response  

            }
            catch (System.Net.WebException)
            {
                throw new HttpException("Request failed, to url :" + requestPath);
            }

        }
        public static WebResponse SendHeartBeat(string SessionKey)
        {

            try
            {
                // Should ONLY be used in test where the certificate is not real.
                ServicePointManager.ServerCertificateValidationCallback +=
                     delegate(
                         object sender,
                         X509Certificate certificate,
                         X509Chain chain,
                         SslPolicyErrors sslPolicyErrors)
                     {
                         return true;
                     };

                // Create the URL
                Uri address = new Uri(RequestUtil.BASE_URL + "/" + RequestUtil.VERSION + "/" + "login" +"/" + SessionKey);

                // Create request
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

                request.Method = HttpMethods.PUT;
                request.Accept = "application/json";
                request.Headers["Authorization"] = "Basic " + System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(SessionKey));
                request.ContentType = "application/x-www-form-urlencoded";

                return request.GetResponse();
                // Get response  

            }
            catch (System.Net.WebException)
            {
                throw new HttpException("HeartBeat failed!");
            }
        }

        public static WebResponse SendRequest(string method, string requestPath, Dictionary<string, string> parameters)
        {
            try
            {
                // Should ONLY be used in test where the certificate is not real.
                ServicePointManager.ServerCertificateValidationCallback +=
                     delegate(
                         object sender,
                         X509Certificate certificate,
                         X509Chain chain,
                         SslPolicyErrors sslPolicyErrors)
                     {
                         return true;
                     };

                // Create the URL
                Uri address = new Uri(RequestUtil.BASE_URL + "/" + RequestUtil.VERSION + "/" + requestPath);

                // Create request
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

                request.Method = "POST";
                request.Accept = "application/json";
                request.ContentType = "application/x-www-form-urlencoded";

                if (parameters != null && (method.Equals(HttpMethods.POST) || method.Equals(HttpMethods.PUT) || method.Equals(HttpMethods.DELETE)))
                {
                    request.ContentType = "application/x-www-form-urlencoded";

                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(nExtRequestUtil.CreateParameterList(parameters));

                    request.ContentLength = byteData.Length;

                    // Write data  
                    using (Stream postStream = request.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                    }
                }


                return request.GetResponse();
                // Get response  

            }
            catch (System.Net.WebException)
            {
                throw new HttpException("Request failed, to url :" + requestPath);
            }

        }
    }
}

