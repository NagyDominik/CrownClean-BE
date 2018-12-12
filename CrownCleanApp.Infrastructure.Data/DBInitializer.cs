using CrownCleanApp.Core.Entity;
using CrownCleanApp.Infrastructure.Data.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Infrastructure.Data
{
    public class DBInitializer
    {

        public static void SeedDB(CrownCleanAppContext ctx, IAuthenticationHelper authenticationHelper)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var testUser = new User() {
                ID = 1,
                Email = "john@mail.dk",
                FirstName = "John",
                LastName = "Johnson",
                IsAdmin = false,
                IsApproved = true,
                PhoneNumber = "+45558587489",
                IsCompany = false,
                Addresses = new List<string>()
                {
                    "7 Sunbrook Drive"
                },
            };

            authenticationHelper.CreatePasswordHash("Password123", out byte[] tpwHash, out byte[] tpwSalt);
            testUser.PasswordHash = tpwHash;
            testUser.PasswordSalt = tpwSalt;

            var testUser2 = new User() {
                ID = 2,
                Email = "mail@mail.dk",
                FirstName = "Grace",
                LastName = "Emms",
                IsAdmin = false,
                IsApproved = false,
                PhoneNumber = "+452312345",
                IsCompany = false,
                Addresses = new List<string>()
                {
                   "715 Barnett Trail",
                   "4 Eliot Junction"
                },
            };

            authenticationHelper.CreatePasswordHash("Password123", out byte[] t1pwHash, out byte[] t1pwSalt);
            testUser2.PasswordHash = t1pwHash;
            testUser2.PasswordSalt = t1pwSalt;

            var admin = new User()
            {
                ID = 3,
                Email = "admin@admin.dk",
                FirstName = "Admin",
                LastName = "Admin",
                IsAdmin = true,
                IsApproved = true,
                IsCompany = false,
                PhoneNumber = "+45552515211",
                Addresses = new List<string>()
                {
                    "Admin address 1"
                },
            };

            authenticationHelper.CreatePasswordHash("Password123", out byte[] passwordHash, out byte[] passwordSalt);
            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;

            var testVehicle = new Vehicle()
            {
                ID = 11,
                Brand = "BMW",
                UniqueID = "ASD123",
                Type = "SUV",
                User = new User() { ID = 1 },
                Size = 2.0f,
                InternalPlus = true
            };

            var testVehicle2 = new Vehicle() {
                ID = 12,
                Brand = "Kia",
                UniqueID = "ASD456",
                Type = "Saloon",
                Size = 1.5f,
                User = new User() { ID = 2 },
                InternalPlus = false
            };

            var testVehicle3 = new Vehicle() {
                ID = 13,
                Brand = "Trabant",
                UniqueID = "EFG123",
                Type = "Saloon",
                Size = 1.0f,
                User = new User() { ID = 2 },
                InternalPlus = false
            };

            var testOrder = new Order() {
                ID = 1,
                ApproveDate = DateTime.Now.AddMonths(-1),
                AtAddress = true,
                Description = "TestDescription",
                OrderDate = DateTime.Now.AddMonths(-1).AddDays(5),
                Services = "testServiceRequired",
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { ID = 12 },
                IsApproved = false

            };

            var testOrder2 = new Order() {
                ID = 2,
                ApproveDate = DateTime.Now.AddMonths(-2),
                AtAddress = true,
                Description = "2222TestDescription",
                OrderDate = DateTime.Now.AddMonths(-2).AddDays(5),
                Services = "2222testServiceRequired",
                User = new User() { ID = 2 },
                Vehicle = new Vehicle() { ID = 12 },
                IsApproved = false
            };

            ctx.Users.Add(admin);
            ctx.Users.Add(testUser);
            ctx.Users.Add(testUser2);

            ctx.Vehicles.Add(testVehicle);
            ctx.Vehicles.Add(testVehicle2);
            ctx.Vehicles.Add(testVehicle3);

            ctx.Orders.Add(testOrder);
            ctx.Orders.Add(testOrder2);

            for (int i = 10; i < 40; i++) {
                var x = new User() {
                    ID = i,
                    Email = "testEmail@fakeemail.dk",
                    FirstName = "Test",
                    LastName = "Testenson",
                    IsAdmin = false,
                    IsApproved = true,
                    PhoneNumber = "+45558587489",
                    IsCompany = false,
                    Addresses = new List<string>(){
                   "asdasd",
                   "weqwdqwe"
                    },
                };
                ctx.Users.Add(x);
            };

            for (int i = 10; i < 40; i++) {
                var y = new Order() {
                    ID = i,
                    ApproveDate = DateTime.Now.AddMonths(-1),
                    AtAddress = true,
                    Description = "TestDescription",
                    OrderDate = DateTime.Now.AddMonths(-1).AddDays(5),
                    Services = "testServiceRequired",
                    User = new User() { ID = 1 },
                    Vehicle = new Vehicle() { ID = 12 },
                    IsApproved = false
                };
                ctx.Orders.Add(y);

            };

            ctx.SaveChanges();
        }
    }
}
