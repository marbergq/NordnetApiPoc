using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using NordNetApiPoC.NordNetAPI.DataContracts;
using NordNetApiPoC.NordNetAPI.StockcsModule;
using NordNetApiPoC.NordNetAPI.News;
using System.Threading;
using NordNetApiPoC.NordNetAPI.AbstractRequests.Accounts;

namespace NordNetApiPoC.NordNetAPI.LoginModule
{
    class nExTApiInfo
    {
       public const int MAX_FAILED_LOGINS = 5;
        // The public key for NEXTAPI from the XML file
        

    }
    class HeartBeatUpdater : IDisposable
    {
        Timer t;
        NordNetApiPoC.NordNetAPI.LoginModule.SesseionExpires Class2Update;
        public HeartBeatUpdater(NordNetApiPoC.NordNetAPI.LoginModule.SesseionExpires Class2Update)
        {
            this.Class2Update = Class2Update;

            t = new Timer(UpdateHeartBeat, null, 0, (Class2Update.SessionExpiration / 2)*1000);
            
        }

        public void UpdateHeartBeat(Object State)
        {
            nExtRequestUtil.SendHeartBeat(Class2Update.SessionKey);
            t.Change(0, (Class2Update.SessionExpiration/2)*1000);
        }

        public void Dispose()
        {
            t.Dispose();
        }
    }


    interface SesseionExpires
    {
         int SessionExpiration { get; set; }
         string SessionKey { get; }
         void UpdateExpiresIn();
         void NotLoggedIn();
    }
    public class Login : SesseionExpires
    {
        public string UserName { private get; set; }
        public string PassWord { private get; set; }
        string PublicKey = LoginClass.PUBLIC_KEY;
        private DateTime SessionExpiresInterval { get; set; }
        private LoginDataContract _UserInfo;
        private int failedLoginCount = 0;
        private HeartBeatUpdater heartBeatService { get; set; }
        private IEnumerable<Account> MyAccount { get; set; }



        private void InitializeMethods()
        {
            if (USERInfo == null)
                throw new Exception("Not logged in");
            failedLoginCount = 0;
            _Stockinfo = new StockInfo { SessionKey = USERInfo.SessionKey };
            _Newsinfo = new NewsInfo { SessionKey = USERInfo.SessionKey };
            _InstrumentsInfo = new Instruments { SessionKey = _UserInfo.SessionKey };
            _AccountInfo = new AccountRequests { SessionKey = _UserInfo.SessionKey, AccountId =  };
            
            if (heartBeatService != null)
                heartBeatService.Dispose();
            heartBeatService = new HeartBeatUpdater(this);
        }

        #region REQUEST MAPPING
        private StockInfo _Stockinfo;
        public StockInfo Stockinfo
        {
            get
            {
                if (failedLoginCount > nExTApiInfo.MAX_FAILED_LOGINS)
                    throw new Exception("Error with account");
                if (DateTime.Compare(DateTime.Now, SessionExpiresInterval) < 0 || !LoggedIn)
                {
                    //valid
                    return _Stockinfo;
                }
                PreformLogin();
                return Stockinfo;
            }
        }

        private NewsInfo _Newsinfo;
        public NewsInfo Newsinfo
        {
            get
            {

                if (failedLoginCount > nExTApiInfo.MAX_FAILED_LOGINS )
                    throw new Exception("Error with account");
                if (DateTime.Compare(DateTime.Now, SessionExpiresInterval) < 0 || !LoggedIn)
                {
                    return _Newsinfo;
                }
                PreformLogin();
                return Newsinfo;
            }
        }
        private Instruments  _InstrumentsInfo;
        public Instruments InstrumentsInfo
        {
            get
            {

                if (failedLoginCount > nExTApiInfo.MAX_FAILED_LOGINS)
                    throw new Exception("Error with account");
                if (DateTime.Compare(DateTime.Now, SessionExpiresInterval) < 0 || !LoggedIn)
                {
                    return _InstrumentsInfo;
                }
                PreformLogin();
                return InstrumentsInfo;
            }
        }


        private AccountRequests _AccountInfo;
        public AccountRequests Accountinfo
        {
            get
            { 
                if (failedLoginCount > nExTApiInfo.MAX_FAILED_LOGINS)
                    throw new Exception("Error with account");
                if (DateTime.Compare(DateTime.Now, SessionExpiresInterval) < 0 || !LoggedIn)
                {
                    return _AccountInfo;
                }
                PreformLogin();
                return Accountinfo;
            }
        }

        #endregion
        private LoginDataContract USERInfo
        {
            get
            {
                if (_UserInfo == null)
                    throw new ArgumentException("not logged in");
                return _UserInfo;

            }
            set
            {
                _UserInfo = value;
                SessionExpiresInterval = DateTime.Now.AddSeconds(value.ExpiresIn);
                SessionExpiration = value.ExpiresIn;
            }
        }

        public bool PreformLogin()
        {
            RSACryptoServiceProvider RSA =
                 new System.Security.Cryptography.RSACryptoServiceProvider();

            // Deserialize the public key
            RSAParameters parameters = nExtRequestUtil.Deserialize<RSAParameters>(PublicKey);

            // Set the public key
            RSA.ImportParameters(parameters);

            // Create timestamp (Unix timestamp in milliseconds)
            string timestamp =
               Math.Round((DateTime.UtcNow -
            new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds).ToString();

            // Base64 encode each field and concatenate with ":" 
            string encoded =
                Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(UserName)) + ":" +
                Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(PassWord)) + ":" +
                Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(timestamp));

            // Encrypt
            byte[] encrypted = RSA.Encrypt(System.Text.Encoding.UTF8.GetBytes(encoded), false);

            // Base 64 encode the blob.
            string blob = Convert.ToBase64String(encrypted);

            Dictionary<string, string> restParameters = new Dictionary<string, string>();
            restParameters["service"] = "NEXTAPI";
            restParameters["auth"] = blob;
            try
            {
                USERInfo = AbstractRequests.AbstractRequestClass.LoginRequest<LoginDataContract>(restParameters);
                InitializeMethods();
                LoggedIn = true;
                return true;
            }
            catch
            {
                failedLoginCount++;
                LoggedIn = false;
                return false;
            }
        }


        private int _SessionExpiration;
        public int SessionExpiration
        {
            get
            {
                return _SessionExpiration;
            }
            set
            {
                _SessionExpiration = value;
            }
        }


        public string SessionKey
        {
            get { return USERInfo.SessionKey; }
        }


        public void UpdateExpiresIn()
        {
            SessionExpiresInterval.AddSeconds(USERInfo.ExpiresIn);
        }

        bool LoggedIn { get; set; }
        public void NotLoggedIn()
        {
            LoggedIn = false;
        }
    }




}
