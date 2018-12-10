using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService.Filtering
{
    public class UserFilter : Filter
    {
        public string Name { get; set; }
        public string Email { get; set; }

        //If false, both company and individual customers alre included. If true, the IsCompany field will decide wich type of user will be filtered.
        public bool FilterToCustomerType { get; set; }

        public bool IsCompany { get; set; }
    }
}
