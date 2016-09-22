using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace carlocaunca.com.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult FallDown()
        {
            return View();
        }
        public ActionResult SpaceInvaders()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return Redirect("https://carlocaunca.wordpress.com/");
        }

        public ActionResult GitHub()
        {
            return Redirect("https://github.com/ccaunca/Projects/tree/master/GameDemos");
        }
    }
}