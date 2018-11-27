using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(CrownCleanAppContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var testUser = new User()
            {
                Email = "testEmail@fakeemail.dk",
                FirstName = "Test",
                LastName = "Testenson",
                IsAdmin = false,
                isApproved = true,
                PhoneNumber = "+45558587489",
                IsCompany = false
            };

            ctx.Users.Add(testUser);

            //var testOrder = new Order()
            //{
                
            //}
            
        }
    }
}
