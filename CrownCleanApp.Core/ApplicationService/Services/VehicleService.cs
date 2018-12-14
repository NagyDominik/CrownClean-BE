using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;

namespace CrownCleanApp.Core.ApplicationService.Services
{
    public class VehicleService : IVehicleService
    {
        readonly IVehicleRepository _repo;

        public VehicleService(IVehicleRepository repo)
        {
            _repo = repo;
        }

        public Vehicle AddVehicle(Vehicle vehicle)
        {
            if (vehicle.ID < 0 || vehicle.ID > 0)
                throw new InvalidDataException("Cannot add vehicle with ID!");
            if (vehicle.UniqueID == null || vehicle.UniqueID.Trim() == "")
                throw new InvalidDataException("Cannot add vehicle without uniqID!");
            if (vehicle.Brand == null || vehicle.Brand.Trim() == "")
                throw new InvalidDataException("Cannot add vehicle without brand!");
            if (vehicle.Type == null || vehicle.Type.Trim() == "")
                throw new InvalidDataException("Cannot add vehicle without type!");
            if (vehicle.User == null || vehicle.User.ID <= 0)
                throw new InvalidDataException("Cannot add vehicle without user!");
            return _repo.Create(vehicle);
        }

        public Vehicle DeleteVehicle(int id)
        {
            if (id <= 0)
                throw new InvalidDataException("Cannot delete vehicle without ID!");
            return _repo.Delete(id);
        }

        public FilteredList<Vehicle> GetAllVehicles(VehicleFilter filter = null)
        {
            return _repo.ReadAll(filter);
        }

        public FilteredList<Vehicle> GetVehiclesOfACustomer(VehicleFilter filter, int id)
        {
            if (id == 0)
                throw new InvalidDataException("Cannot get vehicle by ID without user ID!");
            return _repo.ReadAll(filter);
        }

        public Vehicle GetVehicleByID(int id)
        {
            if (id == 0)
                throw new InvalidDataException("Cannot get vehicle by ID without ID!");
            return _repo.ReadByID(id);
        }

        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            if (vehicle == null || vehicle.ID <= 0)
                throw new InvalidDataException("Cannot update vehicle without ID!");
            return _repo.Update(vehicle);
        }
    }
}
