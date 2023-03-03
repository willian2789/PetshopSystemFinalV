using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.usuName = Session["usuName"];
            ViewBag.usuImage = Session["usuImg"];
            return View();
        }

    }
}