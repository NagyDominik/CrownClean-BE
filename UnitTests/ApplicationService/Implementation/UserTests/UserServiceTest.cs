using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.ApplicationService.Services;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;
using System.Collections;
using System.Linq;

namespace TestCore.ApplicationService.Implementation
{
    /// <summary>
    /// This class is intended to test the normal operation of the UserService.
    /// </summary>
    public class UserServiceTest
    {
        #region MockData

        class IndividualUserTestData : IEnumerable<Object[]>
        {
            readonly User u1 = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Addresses = new List<string>() { "Test Str. 4" },
                Email = "em@ail.dk",
                PhoneNumber = "+4552521130",
                IsCompany = false,
                IsAdmin = false,
                IsApproved = false
            };

           readonly User u2 = new User()
            {
                FirstName = "Tester",
                LastName = "McTested",
                Addresses = new List<string>() { "Test Str. 4", "New Address 2" },
                Email = "e@mail.dk",
                PhoneNumber = "+4552521131",
                IsCompany = false,
                IsAdmin = false,
                IsApproved = false
           };

            readonly User u3 = new User()
            {
                FirstName = "Tester",
                LastName = "McTested Jr.",
                Addresses = new List<string>() { "Test Str. 4", "New Address 2", "Empty str." },
                Email = "ema@il.dk",
                PhoneNumber = "+4552521132",
                IsCompany = false,
                IsAdmin = false,
                IsApproved = false
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { u1 };
                yield return new object[] { u2 };
                yield return new object[] { u3 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        class CorporateUserTestData : IEnumerable<object[]>
        {
            readonly User u1 = new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Addresses = new List<string>() { "Test Company Str. 4" },
                Email = "em@ail.dk",
                PhoneNumber = "+4552521130",
                IsCompany = true,
                IsAdmin = false,
                TaxNumber = "111111-8-9"
            };

            readonly User u2 = new User()
            {
                FirstName = "Tester",
                LastName = "McTested",
                Addresses = new List<string>() { "Test Str. 4", "New Address 2" },
                Email = "e@mail.dk",
                PhoneNumber = "+4552521131",
                IsAdmin = true,
                IsCompany = true,
                TaxNumber = "111111-4-9"
            };

            readonly User u3 = new User()
            {
                FirstName = "Tester",
                LastName = "McTested Jr.",
                Addresses = new List<string>() { "Test Str. 4", "New Address 2", "Empty str." },
                Email = "ema@il.dk",
                PhoneNumber = "+4552521132",
                IsCompany = true,
                IsAdmin = false,
                TaxNumber = "114881-8-9"
            };

            public IEnumerator<Object[]> GetEnumerator()
            {
                yield return new object[] { u1 };
                yield return new object[] { u2 };
                yield return new object[] { u3 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        #endregion

        #region CreateUserTests

        [Theory]
        [ClassData(typeof(IndividualUserTestData))]
        [ClassData(typeof(CorporateUserTestData))]
        public void AddUser(User user)
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            userService.AddUser(user);
            moqRep.Verify(x => x.Create(user), Times.Once);
        }

        [Theory]
        [InlineData("sebok.pete101@gmail.com")]
        [InlineData("testuser@owndomain.dk")]
        [InlineData("david.jones@proseware.com")]
        [InlineData("david.jones@proseware.other.com")]
        [InlineData("useremail.emaiaddress.103@domain.sub.dk")]
        public void CreateUserWithValidEmailAddress(string email)
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User user = new User()
            {
                FirstName = "Test",
                LastName = "User",
                Email = email,
                PhoneNumber = "+4552521120",
                Addresses = new List<string>() { "Address_1", "Address_2"},
                IsAdmin = false,
                IsApproved = false,
                IsCompany = false
            };

            userService.AddUser(user);
            moqRep.Verify(x => x.Create(user), Times.Once);
        }

        #endregion

        #region ApproveUserTest

        [Theory]
        [ClassData(typeof(IndividualUserTestData))]
        [ClassData(typeof(CorporateUserTestData))]
        public void UserApproveTest(User user)
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            user.ID = 1;

            moqRep.Setup(x => x.ReadByID(user.ID)).Returns(user);
            userService.ApproveUser(user.ID);
            moqRep.Verify(x => x.Update(user), Times.Once);
        }

        #endregion

        #region DeleteTest
        [Theory]
        [ClassData(typeof(IndividualUserTestData))]
        [ClassData(typeof(CorporateUserTestData))]
        public void UserDeleteTest(User user)
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            user.ID = 1;

            userService.DeleteUser(1);
            moqRep.Verify(x => x.Delete(1), Times.Once);
        }
        #endregion

        #region GetAllUsersTests

        [Fact]
        public void GetAllUsersTest()
        {
            IndividualUserTestData testData = new IndividualUserTestData();
            var objects = testData.ToList();

            List<User> users = new List<User>();

            foreach (var item in objects)
            {
                users.Add((User)item[0]);
            }

            var moqRep = new Mock<IUserRepository>();
            moqRep.Setup(x => x.ReadAll(null)).Returns(new CrownCleanApp.Core.DomainService.Filtering.FilteredList<User>() { List = users });

            IUserService userService = new UserService(moqRep.Object);

            List<User> retrievedUsers = userService.GetAllUsers(null).List.ToList();
            moqRep.Verify(x => x.ReadAll(null), Times.Once);
            Assert.Equal(users, retrievedUsers);
        }

        #endregion

        #region GetUserByIDTests

        [Fact]
        public void GetUserByIdTest()
        {
            IndividualUserTestData testData = new IndividualUserTestData();
            var objects = testData.ToList();

            List<User> users = new List<User>();

            int i = 0;
            foreach (var item in objects)
            {
                User u = (User)item[0];
                u.ID = i;
                users.Add(u);
                i++;
            }

            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            for (int id = 0; id < objects.Count; id++)
            {
                moqRep.Setup(x => x.ReadByID(id)).Returns(users.FirstOrDefault(u => u.ID == id));
                User retrievedUser = userService.GetUserByID(id);
                moqRep.Verify(x => x.ReadByID(id), Times.Once);
                Assert.Equal(id, retrievedUser.ID);
                moqRep.Reset();
            }  
        }

        #endregion

        #region UpdateUserTests

        [Fact]
        public void UpdateUserTest()
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            User user = new User()
            {
                ID = 1,
                FirstName = "First",
                LastName = "Last",
                Email = "user@mail.dk",
                PhoneNumber = "+4552521130",
                Addresses = new List<string>() { "Address Str. 1" },
                IsCompany = false,
                IsAdmin = false
            };

            moqRep.Setup(x => x.ReadByID(user.ID)).Returns(user);

            userService.UpdateUser(user);
            moqRep.Verify(x => x.Update(user));
        }

        #endregion
    }
}
