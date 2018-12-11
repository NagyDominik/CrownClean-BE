using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService.Filtering
{
    /// <summary>
    /// Abstract filter class that contains the shared fields between different filters.
    /// </summary>
    public abstract class Filter
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
