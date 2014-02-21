using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WallaceFarms.Controllers
{
    public class InfoController : Controller
    {

        public ActionResult Index()
        {
            return View("Info");
        }

    }
}
