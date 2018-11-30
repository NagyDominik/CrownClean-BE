using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Vehicle Create(Vehicle vehicle)
        {
            _ctx.Attach(vehicle).State = EntityState.Added;
            _ctx.SaveChanges();
            return vehicle;
        }

        public Vehicle Delete(int id)
        {
            var removedVehicle = _ctx.Remove(new Vehicle { ID = id }).Entity;
            _ctx.SaveChanges();
            return removedVehicle;
        }

        public IEnumerable<Vehicle> ReadAll()
        {
            return _ctx.Vehicles;
        }

        public Vehicle ReadByID(int id)
        {
            return _ctx.Vehicles.Include(v => v.Orders).Include(v => v.User).AsNoTracking().FirstOrDefault(v => v.ID == id);
        }

        public Vehicle Update(Vehicle vehicle)
        {
            _ctx.Attach(vehicle).State = EntityState.Modified;
            _ctx.Entry(vehicle).Reference(v => v.User).IsModified = true;
            _ctx.SaveChanges();
            return vehicle;

        }
    }
}
