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
                ID = 1,
                Email = "testEmail@fakeemail.dk",
                FirstName = "Test",
                LastName = "Testenson",
                IsAdmin = false,
                IsApproved = true,
                PhoneNumber = "+45558587489",
                IsCompany = false,
                Addresses = new List<string>()
                {
                   "asdasd",
                   "weqwdqwe"
                },
            };
            
            var testUser2 = new User()
            {
                Email = "test2Email@fakeemail.dk",
                FirstName = "Testicy",
                LastName = "Testensonion",
                IsAdmin = true,
                IsApproved = true,
                PhoneNumber = "+4523123",
                IsCompany = false,
                Addresses = new List<string>()
                {
                   "asd street 2"
                },
            };

            var testVehicle = new Vehicle()
            {
                Brand = "BMW",
                UniqueID = "ASD123",
                Type = "SUV",
                User = testUser,
                InternalPlus = true
            };

            var testVehicle2 = new Vehicle()
            {
                Brand = "Kia",
                UniqueID = "ASD123",
                Type = "Saloon",
                User = testUser2,
                InternalPlus = false
            };



            var testOrder = new Order()
            {
                ApproveDate = DateTime.Now.AddMonths(-1),
                AtAddress = true,
                Description = "TestDescription",
                OrderDate = DateTime.Now.AddMonths(-1).AddDays(5),
                Services = "testServiceRequired",
                User = testUser,
                Vehicle = testVehicle,
                IsApproved = true
               
            };

            var testOrder2 = new Order()
            {
                ApproveDate = DateTime.Now.AddMonths(-2),
                AtAddress = true,
                Description = "2222TestDescription",
                OrderDate = DateTime.Now.AddMonths(-2).AddDays(5),
                Services = "2222testServiceRequired",
                User = testUser2,
                Vehicle = testVehicle2,
                IsApproved = true

            };

            ctx.Users.Add(testUser);
            ctx.Users.Add(testUser2);

            ctx.Vehicles.Add(testVehicle);
            ctx.Vehicles.Add(testVehicle2);

            ctx.Orders.Add(testOrder);
            ctx.Orders.Add(testOrder2);

            ctx.SaveChanges();
        }
    }
}
