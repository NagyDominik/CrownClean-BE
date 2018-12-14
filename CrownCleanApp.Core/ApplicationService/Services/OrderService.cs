using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;

namespace CrownCleanApp.Core.ApplicationService.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public Order AddOrder(Order order)
        {
            if (order.ID < 0 || order.ID > 0)
                throw new InvalidDataException("Cannot add order with ID!");
            if (order.User == null || order.User.ID <= 0)
                throw new InvalidDataException("Cannot add order without user!");
            if (order.Vehicle == null || order.Vehicle.ID <= 0)
                throw new InvalidDataException("Cannot add order without vehicle!");
            if (order.Services == null || order.Services.Trim() == "")
                throw new InvalidDataException("Cannot add order without service!");
            order.UserID = order.User.ID;
            order.VehicleID = order.Vehicle.ID;
            order.OrderDate = DateTime.Now;
            return _repo.Create(order);
        }

        public Order ApproveOrder(int id)
        {
            if (id == 0)
                throw new InvalidDataException("Cannot approve order without ID!");

            Order orderUpdate = _repo.ReadByID(id);
            if (orderUpdate != null) {
                if (orderUpdate.IsApproved)
                    throw new InvalidDataException("Cannot approve order with approved status!");
                else { 
                    orderUpdate.IsApproved = true;
                    orderUpdate.ApproveDate = DateTime.Now;
                }
            }
            else
                throw new InvalidDataException("There is no order with the ID of " + id + " in the database!");
            return _repo.Update(orderUpdate);
        }

        public Order DeleteOrder(int id)
        {
            if (id == 0)
                throw new InvalidDataException("Cannot delete order without ID!");
            return _repo.Delete(id);
        }

        public FilteredList<Order> GetAllOrders(OrderFilter filter = null)
        {
            return _repo.ReadAll(filter);
        }

        public Order GetOrderByID(int id)
        {
            if (id == 0)
                throw new InvalidDataException("Cannot get order by ID without ID!");
            Order order = _repo.ReadByID(id);
            return order;
        }

        public FilteredList<Order> GetOrdersOfACustomer(OrderFilter filter, int id)
        {
            if (id == 0)
                throw new InvalidDataException("Cannot get order by ID without ID!");
            var orders = _repo.ReadAll(filter).List.Where(o => o.UserID == id);
            return new FilteredList<Order>() { Count = orders.Count(), List = orders  };
        }

        public Order UpdateOrder(Order order)
        {
           if(order == null || order.ID <= 0)
                throw new InvalidDataException("Cannot update order without ID!");
            return _repo.Update(order);
        }
    }
}
