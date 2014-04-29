using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WallaceFarms;
using WallaceFarms.Models;

namespace WallaceFarms.Models
{
    public class OrderModel
    {
        public Order theOrder { get; set; }
        public BeefOrder theBeefOrder { get; set; }

        public static SelectList quarterOptions
        {
            get 
            {
                var options = new List<QuarterOptions>();

                options.Add(new QuarterOptions(1, "Half of a Half"));
                options.Add(new QuarterOptions(2, "A Half"));
                options.Add(new QuarterOptions(4, "A Whole"));

                return (new SelectList(options, "ID", "Description"));
            }
        }

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