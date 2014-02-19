using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WallaceFarms.Controllers
{
    public class IndexController : Controller
    {
        //DJN & SBH - Created controller to return Index view contained in Views/wallaceFarms
        public ActionResult Index()
        {
            return View();
        }
    }
}
