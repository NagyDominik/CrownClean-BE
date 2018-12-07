using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService.Filtering
{
    /// <summary>
    /// Filter used for pagination
    /// </summary>
    public class Filter
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
