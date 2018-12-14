using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.ApplicationService
{
    public interface IOrderService
    {
        /// <summary>
        /// Save an order to the database.
        /// </summary>
        /// <param name="order">The order that will be saved.</param>
        /// <returns>The saved order.</returns>
        Order AddOrder(Order order);

        /// <summary>
        /// Retrieve all orders from the database, possibly filtered.
        /// </summary>
        /// <param name="filter">A filter object used for pagination and filtering</param>
        /// <returns></returns>
        FilteredList<Order> GetAllOrders(OrderFilter filter);

        /// <summary>
        /// Return the orders of a specific customer
        /// </summary>
        /// <param name="filter">A filter object used for pagination and filtering</param>
        /// <param name="id">The ID of the customer whose orders are returned.</param>
        /// <returns>The orders of the specified user.</returns>
        FilteredList<Order> GetOrdersOfACustomer(OrderFilter filter, int id);

        /// <summary>
        /// Retrieve an order based on ID.
        /// </summary>
        /// <param name="id">The ID of the order that will be retrieved.</param>
        /// <returns>The order with the supplied ID if exists, null otherwise.</returns>
        Order GetOrderByID(int id);

        /// <summary>
        /// Update an already existing order in the database.
        /// </summary>
        /// <param name="order">The order that will be updated.</param>
        /// <returns>The updated order.</returns>
        Order UpdateOrder(Order order);

        /// <summary>
        /// Approve an order with the given ID.
        /// </summary>
        /// <param name="id">The ID of the order that will be approved.</param>
        /// <returns>The approved order if successfull, null otherwise.</returns>
        Order ApproveOrder(int id);

        /// <summary>
        /// Delete an order with the given ID.
        /// </summary>
        /// <param name="id">The ID of the order that will be deleted.</param>
        /// <returns>The deleted order, if existed, null otherwise.</returns>
        Order DeleteOrder(int id);
    }
}
