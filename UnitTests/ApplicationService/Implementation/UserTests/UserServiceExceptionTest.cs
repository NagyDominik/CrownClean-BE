﻿using System;
using System.Collections.Generic;
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
    /// This class is intended to test the conditions, in which the UserService should thréow an exception.
    /// </summary>
    public class UserExceptionTests
    {
        #region UserAddTests

        [Fact]
        public void AddNullUserThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = null;
            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Input is null!", e.Message);
        }

        [Fact]
        public void AddCustomerWithIDThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User() {
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

            User newUser = new User() {
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

            User newUser = new User() {
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

            User newUser = new User() {
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

            User newUser = new User() {
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

            User newUser = new User() {
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

            User newUser = new User() {
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

            User newUser = new User() {
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

        [Theory]
        [InlineData("j.@server1.proseware.com")]
        [InlineData("j..s@proseware.com")]
        [InlineData("js*@proseware.com")]
        [InlineData("js@proseware..com")]
        [InlineData("testemai.dk")]
        [InlineData("tesmail@dk")]
        public void AddUserWithInvalidEmailAddressThrowsException(string email)
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = email,
                PhoneNumber = "+4552521130",
                Addresses = new List<string>() { "Address1" },
                IsAdmin = false,
                IsCompany = false
            };

            Exception e = Assert.Throws<InvalidDataException>(() => userService.AddUser(newUser));
            Assert.Equal("Invalid e-mail address!", e.Message);
        }

        #endregion

        #region UserApproveTests

        [Fact]
        public void ApproveWithoutUserIDThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            int ID = 0;

            Exception e = Assert.Throws<InvalidDataException>(() => userService.ApproveUser(ID));
            Assert.Equal("Cannot approve user without ID!", e.Message);
        }

        [Fact]
        public void ApproveAlreadyApprovedUserThrowsException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User newUser = new User() { ID = 1, IsApproved = true };

            moqRep.Setup(x => x.ReadByID(newUser.ID)).Returns(newUser);

            Exception e = Assert.Throws<InvalidDataException>(() => userService.ApproveUser(newUser.ID));
            Assert.Equal("User is already approved!", e.Message);
        }

        #endregion

        #region DeleteUserTests

        [Fact]
        public void DeleteUserNegativeIDShouldThrowException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            Exception e = Assert.Throws<InvalidDataException>(() => userService.DeleteUser(-1));
            Assert.Equal("No User with negative ID exists!", e.Message);
        }
        #endregion

        #region GetUserByIDTests

        [Fact]
        public void GetUserWithNegativeIDShouldThrowException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            Exception e = Assert.Throws<InvalidDataException>(() => userService.GetUserByID(-1));
            Assert.Equal("No User with negative ID exists!", e.Message);
        }

        #endregion

        #region UpdateUserTests

        [Fact]
        public void UpdateUserNullShouldThrowException()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User user = null;

            Exception e = Assert.Throws<InvalidDataException>(() => userService.UpdateUser(user));
            Assert.Equal("Input is null!", e.Message);

        }

            #endregion

    }
}
