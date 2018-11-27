using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
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

        public Order Create()
        {
            throw new NotImplementedException();
        }

        public Order Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Order ReadByID(int id)
        {
            throw new NotImplementedException();
        }

        public Order Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
