using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.Entity
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string UniqueID { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public float Size { get; set; }
        public bool InternalPlus { get; set; }
        public User User { get; set; }
    }
}
