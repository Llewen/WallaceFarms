using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WallaceFarms.Controllers
{
    public class OrderControllerOriginal : Controller
    {

        public ActionResult Index()
        {
            return View("Order");
        }

    }
}
