using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService
{
    public interface IVehicleRepository
    {
        /// <summary>
        /// Save a vehicle to the database.
        /// </summary>
        /// <param name="vehicle">The vehicle that will be saved to the database.</param>
        /// <returns>The vehicle that was saved to the database.</returns>
        Vehicle Create(Vehicle vehicle);

        /// <summary>
        /// Retrieve a list of vehicles stored in the database.
        /// </summary>
        /// <returns>A list of vehicles stored in the database.</returns>
        FilteredList<Vehicle> ReadAll(Filter filter = null);

        /// <summary>
        /// Retrieve a single vehicle based on the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the vehicle that will be returned.</param>
        /// <returns>The vehicle with the supplied ID if exists, null otherwise.</returns>
        Vehicle ReadByID(int id);

        /// <summary>
        /// Update an already existing vehicle in the database.
        /// </summary>
        /// <param name="vehicle">The vehicle that will be updated.</param>
        /// <returns>The updated vehicle.</returns>
        Vehicle Update(Vehicle vehicle);

        /// <summary>
        /// Delete an existing vehicle from the database.
        /// </summary>
        /// <param name="id">The id of the vehicle to be deleted.</param>
        /// <returns>The deleted vehicle if existed, null otherwise.</returns>
        Vehicle Delete(int id);
    }
}
