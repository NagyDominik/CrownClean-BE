﻿using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public Vehicle DeleteVehicle(int id)
        {
            throw new NotImplementedException();
        }

        public List<Vehicle> GetAllVehicles()
        {
            throw new NotImplementedException();
        }

        public Vehicle GetVehicleByID(int id)
        {
            throw new NotImplementedException();
        }

        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
