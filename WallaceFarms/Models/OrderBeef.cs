using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*namespace WallaceFarms.Models
{
    public class OrderBeef
    {
        [Key]
        public int BeefOrderID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        [Display(Name = "Number of Quarters")]
        public int NumQuarters { get; set; }

        [ForeignKey("Butcher")]
        public int ButcherID { get; set; }
        public virtual Butcher Butcher { get; set; }
    }

    public class OrderBeefDBContext : DbContext
    {
        public DbSet<OrderBeef> BeefOrder { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Butcher> Butcher { get; set; }
    }
}*/