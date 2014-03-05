using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WallaceFarms.Models
{
    public class Customer
    {
        public int CustID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
    }
}