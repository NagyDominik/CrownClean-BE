﻿using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Infrastructure.Data.SQLRepositories
{
    public class VehicleRepository : IVehicleRepository
    {
        readonly CrownCleanAppContext _ctx;

        public VehicleRepository(CrownCleanAppContext ctx)
        {
            _ctx = ctx;
        }

        public Vehicle Create()
        {
            throw new NotImplementedException();
        }

        public Vehicle Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Vehicle ReadByID(int id)
        {
            throw new NotImplementedException();
        }

        public Vehicle Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
