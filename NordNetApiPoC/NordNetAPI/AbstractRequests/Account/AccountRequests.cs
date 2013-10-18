using NordNetApiPoC.NordNetAPI.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordNetApiPoC.NordNetAPI.AbstractRequests.Accounts
{
    public class AccountRequests : AbstractRequestClass
    {
         public IEnumerable<Account> MyAccount
        {
            get
            {
                return MakeRequest<IEnumerable<Account>>(HttpMethods.GET, "accounts");
            }
       }

        public AccountInfo RequestAccount(Account account)
        {
                return MakeRequest<AccountInfo>(HttpMethods.GET, "accounts", account.AccountID);
        }

        public IEnumerable<AccountPosistions> Positions(Account account)
        {
                return MakeRequest<IEnumerable<AccountPosistions>>(HttpMethods.GET, "accounts",  account.AccountID + "/" +"positions" );    
        }

        //public AccountInfo requestDefaultAccount
        //{
        //    get { 
        //        return MakeRequest<AccountInfo>
        //    }
        //}
    }
}
