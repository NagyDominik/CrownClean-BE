using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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

        public FilteredList<Vehicle> ReadAll(VehicleFilter filter)
        {
            FilteredList<Vehicle> filteredList = new FilteredList<Vehicle>();

            if (filter != null)
            {
                #region Filtering

                filteredList.List = _ctx.Vehicles;

                if (!string.IsNullOrEmpty(filter.Brand))
                {
                    filteredList.List = filteredList.List.Where(v => v.Brand.Contains(filter.Brand));
                }

                if (!string.IsNullOrEmpty(filter.Type))
                {
                    filteredList.List = filteredList.List.Where(v => v.Type.Contains(filter.Type));
                }

                if (!string.IsNullOrEmpty(filter.UniqueID))
                {
                    filteredList.List = filteredList.List.Where(v => v.UniqueID.Contains(filter.UniqueID));
                }

                if (filter.FilterSize)
                {
                    if (filter.SmallerThan)
                    {
                        filteredList.List = filteredList.List.Where(v => v.Size < filter.Size);
                    }
                    else
                    {
                        filteredList.List = filteredList.List.Where(v => v.Size > filter.Size);
                    }
                }
                else
                {
                    if (filter.SmallerThan)
                    {
                        throw new InvalidDataException("Filtering by size is not enabled!");
                    }
                }
                #endregion

                #region Pagination

                if (filter.CurrentPage > 0 && filter.ItemsPerPage > 0)
                {
                    filteredList.List = filteredList.List
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                    .Take(filter.ItemsPerPage);
                }

                filteredList.Count = _ctx.Users.Count();

                #endregion
            }
            else
            {
                filteredList.List = _ctx.Vehicles;
                filteredList.Count = filteredList.List.Count();
            }

            return filteredList;
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
