using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Create an order in the database.
        /// </summary>
        /// <returns>The stored order.</returns>
        Order Create(Order order);

        /// <summary>
        /// Return all orders stored in the database.
        /// </summary>
        /// <param name="filter">Used for filtering and pagination.</param>
        /// <returns>All orders stored in the database, filtered using the Filter object.</returns>
        FilteredList<Order> ReadAll(OrderFilter filter);

        /// <summary>
        /// Returns a single order that matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the order that will be returned.</param>
        /// <returns>The order that matches the supplied ID if exist, null otherwise.</returns>
        Order ReadByID(int id);

        /// <summary>
        /// Update a stored order in the database.
        /// </summary>
        /// <param name="order">The order that will be updated.</param>
        /// <returns>The updated order.</returns>
        Order Update(Order order);

        /// <summary>
        /// Delete an order based on ID.
        /// </summary>
        /// <param name="id">The ID of the order that was deleted.</param>
        /// <returns>The deleted order, if existed, null otherwise.</returns>
        Order Delete(int id);
    }
}
