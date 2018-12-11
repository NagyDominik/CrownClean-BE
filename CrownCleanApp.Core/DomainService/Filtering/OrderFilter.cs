using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService.Filtering
{
    public class OrderFilter : Filter
    {
        public int UserID { get; set; }
        public string ServicesSearch { get; set; }
        public string DescriptionSearch { get; set; }
    }
}
