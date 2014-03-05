using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/* Test stuff, delete when not needed */
using System.Data.Entity;

namespace WallaceFarms.Models
{
    public class Order
    {
        public int orderID { get; set; }
        public int CustID { get; set; }
        public int NumQuarters { get; set; }
        public int CutID { get; set; }
    }

    public class OrderDBContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}