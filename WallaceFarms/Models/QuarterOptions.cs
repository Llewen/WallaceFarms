using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallaceFarms.Models
{
    public class QuarterOptions
    {
        public int ID { get; set; }
        public string Description { get; set; }

        public QuarterOptions(int id, string description)
        {
            ID = id;
            Description = description;
        }
    }
}
