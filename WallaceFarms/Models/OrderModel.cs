using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WallaceFarms;

namespace WallaceFarms.Models
{
    public class OrderModel
    {
        public Order theOrder { get; set; }
        public BeefOrder theBeefOrder { get; set; }

        public OrderModel() { }

        public OrderModel(Order order, BeefOrder beefOrder)
        {
            theOrder = order;
            theBeefOrder = beefOrder;
        }
        public OrderModel(Order order)
        {
            theOrder = order;
            theBeefOrder = new BeefOrder();

        }
    }
}