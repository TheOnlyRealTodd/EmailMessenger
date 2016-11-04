using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactFormWithEmail.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index","Form");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return HttpNotFound();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return HttpNotFound();
        }
    }
}