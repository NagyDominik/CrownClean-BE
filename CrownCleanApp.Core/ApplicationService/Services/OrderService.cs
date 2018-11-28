using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CrownCleanApp.Core.DomainService;
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
            return _repo.Create(order);
        }

        public Order ApproveOrder(Order order)
        {
            if (order == null || order.ID == 0)
                throw new InvalidDataException("Cannot approve order without ID!");
            if (order.IsApproved)
                throw new InvalidDataException("Cannot approve order with approved status!");
            Order orderUpdate = _repo.ReadByID(order.ID);
            orderUpdate.IsApproved = true;
            return _repo.Update(orderUpdate);
        }

        public Order DeleteOrder(int id)
        {
            if (id == 0)
                throw new InvalidDataException("Cannot delete order without ID!");
            return _repo.Delete(id);
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByID(int id)
        {
            if (id == 0)
                throw new InvalidDataException("Cannot get order by ID without ID!");
            return _repo.ReadByID(id);
        }

        public Order UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
