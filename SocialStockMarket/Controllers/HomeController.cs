using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialStockMarket.Models.Bank;
using NordnetPoC.Backend.Login;
using NordnetPoC.Backend.Interfaces;
namespace SocialStockMarket.Controllers
{
    public class HomeController : Controller
    {
        private ILoginFactory f = new LoginFactory();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
