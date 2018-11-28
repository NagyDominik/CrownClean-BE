using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CrownCleanApp.Core.DomainService;
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
            throw new NotImplementedException();
        }

        public List<Vehicle> GetAllVehicles()
        {
            return _repo.ReadAll().ToList();
        }

        public Vehicle GetVehicleByID(int id)
        {
            if (id == 0)
                throw new InvalidDataException("Cannot get vehicle by ID without ID!");
            return _repo.ReadByID(id);
        }

        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
