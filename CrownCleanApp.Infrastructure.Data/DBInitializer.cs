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

            var testUser2 = new User() {
                ID = 2,
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
                ID = 11,
                Brand = "BMW",
                UniqueID = "ASD123",
                Type = "SUV",
                User = new User() { ID = 1},
                InternalPlus = true
            };

            var testVehicle2 = new Vehicle()
            {
                ID = 12,
                Brand = "Kia",
                UniqueID = "ASD456",
                Type = "Saloon",
                User = new User() { ID = 2 },
                InternalPlus = false
            };

            var testVehicle3 = new Vehicle() {
                ID = 13,
                Brand = "Trabant",
                UniqueID = "EFG123",
                Type = "Saloon",
                User = new User() { ID = 2 },
                InternalPlus = false
            };

            var testOrder = new Order()
            {
                ID = 1,
                ApproveDate = DateTime.Now.AddMonths(-1),
                AtAddress = true,
                Description = "TestDescription",
                OrderDate = DateTime.Now.AddMonths(-1).AddDays(5),
                Services = "testServiceRequired",
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { ID = 11 },
                IsApproved = true
               
            };

            var testOrder2 = new Order()
            {
                ID = 2,
                ApproveDate = DateTime.Now.AddMonths(-2),
                AtAddress = true,
                Description = "2222TestDescription",
                OrderDate = DateTime.Now.AddMonths(-2).AddDays(5),
                Services = "2222testServiceRequired",
                User = new User() { ID = 2 },
                Vehicle = new Vehicle() { ID = 12 },
                IsApproved = true
            };

            ctx.Users.Add(testUser);
            ctx.Users.Add(testUser2);

            ctx.Vehicles.Add(testVehicle);
            ctx.Vehicles.Add(testVehicle2);
            ctx.Vehicles.Add(testVehicle3);

            ctx.Orders.Add(testOrder);
            ctx.Orders.Add(testOrder2);

            ctx.SaveChanges();
        }
    }
}
