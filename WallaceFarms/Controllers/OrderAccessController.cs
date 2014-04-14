using System.Linq;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using WallaceFarms;
using System;

namespace WallaceFarms.Controllers
{
    public class OrderAccessController : OrderController
    {
        
        public ActionResult Index()
        {
            var orders =
                from o in entities.Orders
                orderby o.OrderID descending
                select o;

            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            int ID;
            try
            {
                ID = (from o in entities.Orders select o.OrderID).Max();
            }
            catch
            {
                ID = 0;
            }
            var order = new Order();
            order.OrderID = ID + 1;
            return View(order);
        }

        [HttpPost]
        public ActionResult Create(Order Order)
        {
            entities.Orders.Add(Order);
            entities.SaveChanges();

            return Redirect("/OrderAccess/Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            var order = entities.Orders
                .SingleOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(int id, Order order)
        {
            var dbOrder = entities.Orders
                .SingleOrDefault(o => o.OrderID == id);

            dbOrder.Name = order.Name;
            dbOrder.Phone = order.Phone;
            dbOrder.Email = order.Email;
            dbOrder.Status = order.Status;

            entities.SaveChanges();

            return Redirect("/OrderAccess/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id = -1)
        {
            var order = entities.Orders
                .SingleOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        [HttpPost]
        public ActionResult Delete(int id, Order order)
        {
            var toDelete = entities.Orders.SingleOrDefault(o => o.OrderID == id);
            entities.Orders.Remove(toDelete);
            entities.SaveChanges();

            return Redirect("/OrderAccess/Index");
        }

    }
}