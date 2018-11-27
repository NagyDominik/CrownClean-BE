using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.Entity
{
    public class Order
    {
        public int ID { get; set; }
        public User User { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ApproveDate { get; set; }
        public string Services { get; set; }
        public string Description { get; set; }
        public bool atAddress { get; set; }
        public bool isApproved { get; set; }
    }
}
