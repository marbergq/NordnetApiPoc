using NordnetPoC.Backend.Interfaces;
using NordnetPoC.Backend.Login;
using SocialStockMarket.DBModels.Context;
using SocialStockMarket.Models.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialStockMarket.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        //
        // GET: /Bank/

        ILoginFactory f;
        private ILoginFactory LoginFactory
        {
            get 
            {
                if (f == null)
                    f = new LoginFactory();
                return f;
            }
        }



        [HttpPost]
        public ActionResult BankLogin(BankLoginModel model)
        {
            

                if (!ModelState.IsValid)
                    return View("BankLogin");
                var customer = LoginFactory.CreateLogin(model.UserName, model.Password, model.Key, model.Bank);
                using (var bankDb = new BankDbContext())
                {
                    var user = bankDb.BankUsers.Single(usr => usr.UserName.ToLower() == User.Identity.Name.ToLower());
                    if (!user.StockFolders.Any())
                    {
                        user.StockFolders.Add(
                            new DBModels.Folder
                            { 
                                FolderName=model.Bank.ToString(),
                                StocksInFolder=customer.Stocks.Select(s=>new DBModels.UserStockForDb(s)).Cast<DBModels.UserStock>().ToList()
                            }
                        );
                    }
                }
                return View("UserInfo", customer);

        }

        //
        // GET: /Bank/BankLogin
        [HttpGet]
        public ActionResult BankLogin()
        {
            return View(new BankLoginModel());
        }

    }
}
