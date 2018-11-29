using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrownCleanApp.Infrastructure.Data.SQLRepositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly CrownCleanAppContext _ctx;

        public OrderRepository(CrownCleanAppContext ctx)
        {
            _ctx = ctx;
        }

        public Order Create(Order order)
        {
            _ctx.Attach(order).State = EntityState.Added;
            _ctx.SaveChanges();
            return order;
        }

        public Order Delete(int id)
        {
            Order order = _ctx.Orders.Remove(new Order { ID = id }).Entity;
            _ctx.SaveChanges();
            return order;
        }

        public IEnumerable<Order> ReadAll()
        {
            return _ctx.Orders;
        }

        public Order ReadByID(int id)
        {
            return _ctx.Orders.Include(o => o.User).Include(o => o.Vehicle).FirstOrDefault(o => o.ID == id);
        }

        public Order Update(Order order)
        {
            _ctx.Attach(order).State = EntityState.Modified;
            _ctx.Entry(order).Reference(o => o.User).IsModified = true;
            _ctx.SaveChanges();
            return order;
        }
    }
}
