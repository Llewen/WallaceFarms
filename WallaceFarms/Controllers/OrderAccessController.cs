using System.Linq;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using WallaceFarms;
using WallaceFarms.Models;
using System;
using System.Collections.Generic;

namespace WallaceFarms.Controllers
{
    public class OrderAccessController : OrderController
    {
        
        public ActionResult Index()
        {
            List<OrderModel> orderList = new List<OrderModel>();
            var orders =
                from o in entities.Orders
                orderby o.OrderID descending
                select o;

            var beefOrders =
                from b in entities.BeefOrders
                orderby b.BeefOrderID descending
                select b;

            foreach (var order in orders)
            {
                bool hasBeefOrder = false;
                foreach (var beefOrder in beefOrders) 
                {
                    if (beefOrder.OrderID == order.OrderID)
                    {
                        orderList.Add(new OrderModel(order, beefOrder));
                        hasBeefOrder = true;
                    }
                }
                if (!hasBeefOrder)
                {
                    orderList.Add(new OrderModel(order));
                }
            }

            return View(orderList);
        }

        [HttpGet]
        public ActionResult Create()
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
            order.theBeefOrder.OrderID=order.theOrder.OrderID;
            return View(order);
        }

        [HttpPost]
        public ActionResult Create(OrderModel Order)
        {
            entities.Orders.Add(Order.theOrder);
            entities.BeefOrders.Add(Order.theBeefOrder);
            entities.SaveChanges();

            return Redirect("/OrderAccess/Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            OrderModel order = new OrderModel();
                
            order.theOrder = entities.Orders
                .SingleOrDefault(o => o.OrderID == id);


            if (order.theOrder == null)
            {
                return HttpNotFound();
            }

            order.theBeefOrder = entities.BeefOrders
                .SingleOrDefault(o => o.OrderID == id);

            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(int id, OrderModel order)
        {
            var dbOrder = entities.Orders
                .SingleOrDefault(o => o.OrderID == id);

            dbOrder.Name = order.theOrder.Name;
            dbOrder.Phone = order.theOrder.Phone;
            dbOrder.Email = order.theOrder.Email;
            dbOrder.Status = order.theOrder.Status;

            var dbBeefOrder = entities.BeefOrders
                .SingleOrDefault(o => o.OrderID == id);

            dbBeefOrder.NumQuarters = order.theBeefOrder.NumQuarters;

            entities.SaveChanges();

            return Redirect("/OrderAccess/Index");
        }

        [HttpGet]
        public ActionResult Delete(int id = -1)
        {
            OrderModel order = new OrderModel();

            order.theOrder = entities.Orders
                .SingleOrDefault(o => o.OrderID == id);

            if (order.theOrder == null)
            {
                return HttpNotFound();
            }

            order.theBeefOrder = entities.BeefOrders
                .SingleOrDefault(o => o.OrderID == id);

            return View(order);
        }

        [HttpPost]
        public ActionResult Delete(int id, OrderModel order)
        {
            var toDeleteOrder = entities.Orders.SingleOrDefault(o => o.OrderID == id);
            var toDeleteBeefOrder = entities.BeefOrders.SingleOrDefault(o => o.OrderID == id);
            entities.Orders.Remove(toDeleteOrder);
            entities.BeefOrders.Remove(toDeleteBeefOrder);
            entities.SaveChanges();

            return Redirect("/OrderAccess/Index");
        }

    }
}