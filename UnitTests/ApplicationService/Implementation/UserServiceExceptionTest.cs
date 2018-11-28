using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.ApplicationService.Services;
using CrownCleanApp.Core.Entity;
using System.IO;

namespace TestCore.ApplicationService.Implementation
{
    /// <summary>
    /// Thi class is intended to test the conditions, in which the UserService should thréow an exception.
    /// </summary>
    public class UserExceptionTests
    {
        #region UserAddTests
        [Fact]
        public void AddCustomerWithIDThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User()
            {
                ID = 1,
                FirstName = "Test",
                LastName = "Test",
                Addresses = new List<string>() { "Address1" },
                Email = "em@ail.dk",
                PhoneNumber = "+4552521130",
                IsAdmin = false,
                IsCompany = false
            };
            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add user with existing ID!", e.Message);
        }

        [Fact]
        public void AddCustomerWithoutFirstNameThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User()
            {
                LastName = "Test",
                Addresses = new List<string>() { "Address1" },
                Email = "em@ail.dk",
                PhoneNumber = "+4552521130",
                IsAdmin = false,
                IsCompany = false
            };

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add a user without first name!", e.Message);
        }

        [Fact]
        public void AddCustomerWithoutLastNameThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User()
            {
                FirstName = "Test",
                Addresses = new List<string>() { "Address1" },
                Email = "em@ail.dk",
                PhoneNumber = "+4552521130",
                IsAdmin = false,
                IsCompany = false
            };

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add a user without last name!", e.Message);
        }

        [Fact]
        public void AddCustomerWithoutPhoneNumberThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Addresses = new List<string>() { "Address1" },
                Email = "em@ail.dk",
                IsAdmin = false,
                IsCompany = false
            };

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add a user without a phone number!", e.Message);
        }

        [Fact]
        public void AddCustomerWithoutEmailThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Addresses = new List<string>() { "Address1" },
                PhoneNumber = "+4552521130",
                IsAdmin = false,
                IsCompany = false
            };

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add a user without an email address!", e.Message);
        }

        [Fact]
        public void AddCustomerWithoutAdressesThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "em@ail.dk",
                PhoneNumber = "+4552521130",
                IsAdmin = false,
                IsCompany = false
            };

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add a user without at least one address!", e.Message);
        }

        [Fact]
        public void AddIndividualCustomerWithTaxNumberThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "em@ail.dk",
                PhoneNumber = "+4552521130",
                Addresses = new List<string>() { "Address1" },
                IsAdmin = false,
                IsCompany = false,
                TaxNumber = "1111-2222-3333-4444"
            };

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add a user with a tax number! Did you mean to add a company instead?", e.Message);
        }

        [Fact]
        public void AddCorporateCustomerWithoutTaxNumberThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "em@ail.dk",
                PhoneNumber = "+4552521130",
                Addresses = new List<string>() { "Address1" },
                IsAdmin = false,
                IsCompany = true
            };

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add a company without tax number! Did you mean to add an individual customer instead?", e.Message);
        }

        #endregion

        #region OrderAddTests

        [Fact]
        public void AddOrderWithIDThrowsException()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() { ID = 1 };
            Order newOrder2 = new Order() { ID = -1 };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder));
            Exception e2 = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder2));
            Assert.Equal("Cannot add order with ID!", e.Message);
            Assert.Equal("Cannot add order with ID!", e2.Message);
        }

        [Fact]
        public void AddOrderWithoutUserThrowsException()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() { User = new User() { }, Vehicle = new Vehicle() { ID = 1} };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder));
            Assert.Equal("Cannot add order without user!", e.Message);
        }

        [Fact]
        public void AddOrderWithoutVehicleThrowsException()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() {
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { }                
            };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder));
            Assert.Equal("Cannot add order without vehicle!", e.Message);
        }

        [Fact]
        public void AddOrderWithoutServiceThrowsException()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() {
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { ID = 7 },
                Services = ""
            };

            Order newOrder2 = new Order() {
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { ID = 7 },
                Services = null
            };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder));
            Exception e2 = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder2));
            Assert.Equal("Cannot add order without service!", e.Message);
            Assert.Equal("Cannot add order without service!", e2.Message);
        }

        #endregion

        #region OrderApproveTests

        [Fact]
        public void ApproveOrderWithNullOrder()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() { ID = 0 };
            Order newOrder2 = null;

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.ApproveOrder(newOrder));
            Exception e2 = Assert.Throws<InvalidDataException>(() => orderService.ApproveOrder(newOrder2));
            Assert.Equal("Cannot approve order without ID!", e.Message);
            Assert.Equal("Cannot approve order without ID!", e2.Message);
        }

        [Fact]
        public void ApproveOrderWithisApprovedTrue()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() { ID = 1, IsApproved = true };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.ApproveOrder(newOrder));
            Assert.Equal("Cannot approve order with approved status!", e.Message);
        }

        #endregion

        #region OrderDeleteTests

        [Fact]
        public void DeleteOrderWithoutID()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.DeleteOrder(0));
            Assert.Equal("Cannot delete order without ID!", e.Message);
        }

        #endregion

        #region OrderDeleteTests

        [Fact]
        public void GetOrderByIDWithoutID()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.GetOrderByID(0));
            Assert.Equal("Cannot get order by ID without ID!", e.Message);
        }

        #endregion
    }
}
