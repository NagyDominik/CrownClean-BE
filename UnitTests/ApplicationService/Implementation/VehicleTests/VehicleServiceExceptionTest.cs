using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.ApplicationService.Services;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace TestCore.ApplicationService.Implementation
{
public class VehicleServiceExceptionTest
    {
        #region VehicleAddTests

        [Fact]
        public void AddVehiclerWithIDThrowsException()
        {
            var moqRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(moqRep.Object);

            Vehicle newVehicle = new Vehicle() { ID = 1 };
            Vehicle newVehicle2 = new Vehicle() { ID = -1 };

            Exception e = Assert.Throws<InvalidDataException>(() => vehicleService.AddVehicle(newVehicle));
            Exception e2 = Assert.Throws<InvalidDataException>(() => vehicleService.AddVehicle(newVehicle2));
            Assert.Equal("Cannot add vehicle with ID!", e.Message);
            Assert.Equal("Cannot add vehicle with ID!", e2.Message);
        }

        [Fact]
        public void AddVehicleWithoutUniqIDThworsException()
        {
            var moqRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(moqRep.Object);

            Vehicle newVehicle = new Vehicle() {UniqueID = null};
            Vehicle newVehicle2 = new Vehicle() { UniqueID = "" };



            Exception e = Assert.Throws<InvalidDataException>(() => vehicleService.AddVehicle(newVehicle));
            Exception e2 = Assert.Throws<InvalidDataException>(() => vehicleService.AddVehicle(newVehicle2));
            Assert.Equal("Cannot add vehicle without uniqID!", e.Message);
            Assert.Equal("Cannot add vehicle without uniqID!", e2.Message);
        }

        [Fact]
        public void AddVehicleWithoutBrandThworsException()
        {
            var moqRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(moqRep.Object);

            Vehicle newVehicle = new Vehicle() { UniqueID = "21341-a",Brand = null };
            Vehicle newVehicle2 = new Vehicle() { UniqueID = "21341-a", Brand = "" };



            Exception e = Assert.Throws<InvalidDataException>(() => vehicleService.AddVehicle(newVehicle));
            Exception e2 = Assert.Throws<InvalidDataException>(() => vehicleService.AddVehicle(newVehicle2));
            Assert.Equal("Cannot add vehicle without brand!", e.Message);
            Assert.Equal("Cannot add vehicle without brand!", e2.Message);
        }

        [Fact]
        public void AddVehicleWithoutTypeThworsException()
        {
            var moqRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(moqRep.Object);

            Vehicle newVehicle = new Vehicle() { UniqueID = "21341-a", Brand = "BMW", Type = null};
            Vehicle newVehicle2 = new Vehicle() { UniqueID = "21341-a", Brand = "BMW", Type = "" };



            Exception e = Assert.Throws<InvalidDataException>(() => vehicleService.AddVehicle(newVehicle));
            Exception e2 = Assert.Throws<InvalidDataException>(() => vehicleService.AddVehicle(newVehicle2));
            Assert.Equal("Cannot add vehicle without type!", e.Message);
            Assert.Equal("Cannot add vehicle without type!", e2.Message);
        }

        [Fact]
        public void AddVehicleWithoutUserThworsException()
        {
            var moqRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(moqRep.Object);

            Vehicle newVehicle = new Vehicle()
            {
                UniqueID = "21341-a",
                Brand = "BMW",
                Type = "SUV",
                User = new User() { }

            };

            Exception e = Assert.Throws<InvalidDataException>(() => vehicleService.AddVehicle(newVehicle));
            Assert.Equal("Cannot add vehicle without user!", e.Message);
        }



        #endregion

        #region VehicleGetByIDTest

        [Fact]
        public void GetVehicleByIDWithoutID()
        {
            var moqRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(moqRep.Object);

            Exception e = Assert.Throws<InvalidDataException>(() => vehicleService.GetVehicleByID(0));
            Assert.Equal("Cannot get vehicle by ID without ID!", e.Message);
        }

        #endregion

        #region VehicleDeleteTest

        [Fact]
        public void DeleteOrderWithoutID()
        {
            var moqRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(moqRep.Object);

            Exception e = Assert.Throws<InvalidDataException>(() => vehicleService.DeleteVehicle(0));
            Assert.Equal("Cannot delete vehicle without ID!", e.Message);
        }

        #endregion

        #region VehicleUpdateTest

        [Fact]
        public void UpdateVehicleWithoutID()
        {
            var moqRep = new Mock<IVehicleRepository>();
            IVehicleService vehicleService = new VehicleService(moqRep.Object);

            Vehicle newVehicle = new Vehicle() { ID = 0 };
            Vehicle newVehicle2 = null;

            Exception e = Assert.Throws<InvalidDataException>(() => vehicleService.UpdateVehicle(newVehicle));
            Exception e2 = Assert.Throws<InvalidDataException>(() => vehicleService.UpdateVehicle(newVehicle2));
            Assert.Equal("Cannot update vehicle without ID!", e.Message);
            Assert.Equal("Cannot update vehicle without ID!", e2.Message);
        }
        #endregion

    }
}
