using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.ApplicationService
{
    public interface IVehicleService
    {
        /// <summary>
        /// Save a vehicle to the database.
        /// </summary>
        /// <param name="vehicle">The vehicle that will be saved.</param>
        /// <returns>The saved vehicle.</returns>
        Vehicle AddVehicle(Vehicle vehicle);

        /// <summary>
        /// Retrieve all vehicles from the database, possibly filtered.
        /// </summary>
        /// <param name="filter">A filter object used for pagination and filtering</param>
        /// <returns></returns>
        FilteredList<Vehicle> GetAllVehicles(VehicleFilter filter);

        /// <summary>
        /// Retrieve an vehicle based on ID.
        /// </summary>
        /// <param name="id">The ID of the vehicle that will be retrieved.</param>
        /// <returns>The vehicle with the supplied ID if exists, null otherwise.</returns>
        Vehicle GetVehicleByID(int id);

        /// <summary>
        /// Return the vehicles of a specific customer
        /// </summary>
        /// <param name="filter">A filter object used for pagination and filtering</param>
        /// <param name="id">The ID of the customer whose vehicles are returned.</param>
        /// <returns>The vehicles of the specified user.</returns>
        FilteredList<Vehicle> GetVehiclesOfACustomer(VehicleFilter filter, int id);

        /// <summary>
        /// Update an already existing vehicle in the database.
        /// </summary>
        /// <param name="vehicle">The vehicle that will be updated.</param>
        /// <returns>The updated vehicle.</returns>
        Vehicle UpdateVehicle(Vehicle vehicle);

        /// <summary>
        /// Delete an vehicle with the given ID.
        /// </summary>
        /// <param name="id">The ID of the vehicle that will be deleted.</param>
        /// <returns>The deleted vehicle, if existed, null otherwise.</returns>
        Vehicle DeleteVehicle(int id);
    }
}
