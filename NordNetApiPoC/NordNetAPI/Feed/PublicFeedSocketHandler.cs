
using NordNetApiPoC.NordNetAPI.LoginModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Web;
using System.Net;

// Added assembly: system.web and system.xml
using System.Net.Security;

// Added for straem
using System.Collections;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;


namespace NordNetApiPoC.NordNetAPI.Feed
{

     class FeedHandler
    {
        public delegate bool GE(object obj, X509Certificate cert, X509Chain chain, SslPolicyErrors errors);
 
        public static bool GETIT(object obj, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
    {
        return true;
    }
        public static SslStream ConnectPrivateFeed(Login myLogin)
        {
            // Create a TCP/IP client socket.
            TcpClient myClient = new TcpClient(myLogin.USERInfo.PublicFeed.HostName, 443); ;

            Console.WriteLine("Client connected to private feed on " + myLogin.USERInfo.PublicFeed.HostName);
            GE ge = GETIT;
            // Create an SSL stream that will close the client's stream.
            SslStream mySslStream = new SslStream(
                myClient.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ge),
                null
                );
            // The server name must match the name on the server certificate.
            try
            {
                mySslStream.AuthenticateAsClient(myLogin.USERInfo.PublicFeed.HostName);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);

                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                //client.Close();
                //return;

            }

            string jsonLogin = "{\"cmd\":\"login\",\"args\":{ \"session_key\":\"" + myLogin.USERInfo.SessionKey + "\",\"service\":\"NEXTAPI\"}}\n"; // MY_APP
            Console.WriteLine("Logging in to feed with " + jsonLogin);
            byte[] messsage = Encoding.UTF8.GetBytes(jsonLogin);
            // Send hello message to the server.

            mySslStream.Write(messsage);
          mySslStream.Flush();

//var subscribe = "{\"cmd\":\"subscribe\",\"args\":{\"t\":\"price\",\"m\":11,\"i\":\"101\"}}\n";
//    messsage = Encoding.UTF8.GetBytes(subscribe);
//                mySslStream.Write(messsage);
//            mySslStream.Flush();

            return mySslStream;
        }


    }
}
