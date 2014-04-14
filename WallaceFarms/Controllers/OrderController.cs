using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WallaceFarms;

namespace WallaceFarms.Controllers
{
    public abstract class OrderController : Controller
    {
        protected OrderEntities entities = new OrderEntities();

        protected override void Dispose(bool disposing)
        {
            entities.Dispose();
            base.Dispose(disposing);
        }

    }
}
