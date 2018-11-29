using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Collections;
using CrownCleanApp.Core.Entity;

namespace TestCore.ApplicationService.Implementation
{
    public class VehicleServiceTest
    {
        #region MockData

        class MockVehicles : IEnumerable<Object[]>
        {
            readonly static User vehicleUser1 = new User()
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

            readonly static User vehicleUser2 = new User()
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

            readonly Vehicle v1 = new Vehicle()
            {
                Brand = "BMW",
                Type = "Car",
                Size = 2.0f,
                InternalPlus = false,
                UniqueID = "ABC-123",
                User = vehicleUser1
            };

            readonly Vehicle v2 = new Vehicle()
            {
                Brand = "Audi",
                Type = "Car",
                Size = 2.5f,
                InternalPlus = true,
                UniqueID = "CBA-321",
                User = vehicleUser1
            };

            readonly Vehicle v3 = new Vehicle()
            {
                Brand = "Tesla",
                Type = "Car",
                Size = 2.8f,
                InternalPlus = true,
                UniqueID = "LOL-GAS",
                User = vehicleUser2
            };

            readonly Vehicle v4 = new Vehicle()
            {
                Brand = "Titanic",
                Type = "Ship",
                Size = 269f,
                InternalPlus = true,
                UniqueID = "ICBRG-123345",
                User = vehicleUser2
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { v1 };
                yield return new object[] { v2 };
                yield return new object[] { v3 };
                yield return new object[] { v4 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        }

        #endregion

        #region AddVehicleTests

        [Theory]
        [ClassData(typeof(MockVehicles))]
        public void AddVehicle(Vehicle vehicle)
        {

        }

        #endregion
    }
}
