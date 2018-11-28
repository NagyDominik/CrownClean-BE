using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.ApplicationService.Services;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;
using System.IO;
using System.Collections;

namespace TestCore.ApplicationService.Implementation
{
    /// <summary>
    /// This class is intended to test the normal operation of the UserService.
    /// </summary>
    public class UserServiceTest
    {
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
                IsAdmin = false
            };

           readonly User u2 = new User()
            {
                FirstName = "Tester",
                LastName = "McTested",
                Addresses = new List<string>() { "Test Str. 4", "New Address 2" },
                Email = "e@mail.dk",
                PhoneNumber = "+4552521131",
                IsCompany = false,
                IsAdmin = false
            };

            readonly User u3 = new User()
            {
                FirstName = "Tester",
                LastName = "McTested Jr.",
                Addresses = new List<string>() { "Test Str. 4", "New Address 2", "Empty str." },
                Email = "ema@il.dk",
                PhoneNumber = "+4552521132",
                IsCompany = false,
                IsAdmin = false
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { u1 };
                yield return new object[] { u2 };
                yield return new object[] { u3 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(IndividualUserTestData))]
        public void AddIndividualUser(User user)
        {
            var moqRep = new Mock<IUserRepository>();
            IUserService userService = new UserService(moqRep.Object);

            userService.AddUser(user);
            moqRep.Verify(x => x.Create(user), Times.Once);
        }
    }
}
