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
    public class UserExceptionTests
    {
        #region UserAddTests
        [Fact]
        public void AddCustomerWithIDThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User() { ID = 1 };

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add user with existing ID!", e.Message);
        }

        [Fact]
        public void AddCustomerWithoutFirstNameThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User() { LastName = "Test"};

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Cannot add a user without first name!", e.Message);
        }

        [Fact]
        public void AddCustomerWithoutLastNameThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User() { FirstName = "Test" };

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
    }
}
