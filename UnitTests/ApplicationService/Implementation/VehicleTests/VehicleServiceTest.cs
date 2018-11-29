using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Collections;
using CrownCleanApp.Core.Entity;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.ApplicationService.Services;
using System.Linq;

namespace TestCore.ApplicationService.Implementation
{
    public class VehicleServiceTest
    {
        #region MockData

        class MockVehicles : IEnumerable<Object[]>
        {
            readonly static User vehicleUser1 = new User()
            {
                ID = 1,
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
                ID = 2,
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
        public void AddVehicleTest(Vehicle vehicle)
        {
            var mockRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(mockRep.Object);

            vehicleService.AddVehicle(vehicle);
            mockRep.Verify(x => x.Create(vehicle), Times.Once);
        }

        #endregion

        #region GetAllVehiclesTests

        [Fact]
        public void GetAllVehicleTest()
        {
            MockVehicles testData = new MockVehicles();
            var objects = testData.ToList();

            List<Vehicle> vehicles = new List<Vehicle>();

            foreach (var item in objects)
            {
                vehicles.Add((Vehicle)item[0]);
            }

            var mockRepo = new Mock<IVehicleRepository>();
            mockRepo.Setup(x => x.ReadAll()).Returns(vehicles);

            IVehicleService userService = new VehicleService(mockRepo.Object);
            List<Vehicle> retrievedVehicles = userService.GetAllVehicles();

            mockRepo.Verify(x => x.ReadAll(), Times.Once);
            Assert.Equal(vehicles, retrievedVehicles);

        }
        #endregion

        #region GetVehicleByIDTests

        [Fact]
        public void GetVehicleByIdTest()
        {
            MockVehicles testData = new MockVehicles();
            var objects = testData.ToList();

            List<Vehicle> vehicles = new List<Vehicle>();

            int i = 1;
            foreach (var item in objects)
            {
                Vehicle u = (Vehicle)item[0];
                u.ID = i;
                vehicles.Add(u);
                i++;
            }

            var moqRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(moqRep.Object);

            for (int id = 1; id < objects.Count; id++)
            {
                moqRep.Setup(x => x.ReadByID(id)).Returns(vehicles.FirstOrDefault(u => u.ID == id));
                Vehicle retrievedVehicle = vehicleService.GetVehicleByID(id);
                moqRep.Verify(x => x.ReadByID(id), Times.Once);
                Assert.Equal(id, retrievedVehicle.ID);
                moqRep.Reset();
            }
        }
        #endregion

        #region UpdateVehicleTests

        [Fact]
        public void UpdateUserTest()
        {
            var mockRepo = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(mockRepo.Object);

            Vehicle vehicle = new Vehicle()
            {
                ID = 1,
                Brand = "BMW",
                Size = 2.0f,
                InternalPlus = true,
                UniqueID = "ABC-123",
                Type = "Car",
                User = new User() { ID = 1 }
            };

            vehicleService.UpdateVehicle(vehicle);
            mockRepo.Verify(x => x.Update(vehicle), Times.Once);
        }

        #endregion

        #region DeleteVehicleTests
          
        [Fact]
        public void DeleteVehicleTest()
        {
            var mockRepo = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(mockRepo.Object);

            Vehicle vehicle = new Vehicle()
            {
                ID = 1,
                Brand = "BMW",
                Size = 2.0f,
                InternalPlus = true,
                UniqueID = "ABC-123",
                Type = "Car",
                User = new User() { ID = 1 }
            };

            vehicleService.DeleteVehicle(1);
            mockRepo.Verify(x => x.Delete(1), Times.Once);
        }

        #endregion
    }
}
