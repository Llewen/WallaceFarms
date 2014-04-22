using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WallaceFarms;
using WallaceFarms.Models;

namespace WallaceFarms.Controllers
{
    public class OrderPageController : OrderController
    {

        [HttpGet]
        public ActionResult Index()
        {
            int ID;
            int BeefID;

            try
            {
                ID = (from o in entities.Orders select o.OrderID).Max();
            }
            catch
            {
                ID = 0;
            }

            try
            {
                BeefID = (from o in entities.BeefOrders select o.BeefOrderID).Max();
            }
            catch
            {
                BeefID = 0;
            }
            OrderModel order = new OrderModel();
            order.theOrder = new Order();
            order.theBeefOrder = new BeefOrder();

            order.theOrder.OrderID = ID + 1;
            order.theBeefOrder.BeefOrderID = BeefID + 1;
            order.theBeefOrder.OrderID = order.theOrder.OrderID;
            return View("OrderPage", order);
        }


        [HttpPost]
        public ActionResult Index(OrderModel Order)
        {
            entities.Orders.Add(Order.theOrder);
            entities.BeefOrders.Add(Order.theBeefOrder);
            entities.SaveChanges();

            return Redirect("/Index");
        }

    }
}
