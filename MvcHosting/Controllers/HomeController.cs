using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Crow.Library.BusinessFactory;

namespace MvcHosting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {

            var data = Business.Get<IBusiness>().GetDataById(3);
            return View();
        }
    }
}
