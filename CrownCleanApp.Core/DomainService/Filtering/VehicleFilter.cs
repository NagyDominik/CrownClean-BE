using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService.Filtering
{
    public class VehicleFilter : Filter
    {
        //TODO: figure out a better way to filter size

        public string UniqueID { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        //Allow filtering by size
        public bool FilterSize { get; set; }
        //Must be 0 if FilterSize is false
        public int Size { get; set; }
        //If true, return only vehicles that are smaller than the specified size.
        public bool SmallerThan { get; set; }
    }   
}
