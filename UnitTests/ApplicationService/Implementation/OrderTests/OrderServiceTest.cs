using System.Collections.Generic;
using Xunit;
using Moq;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.ApplicationService.Services;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.Entity;
using System.Collections;
using System;

namespace TestCore.ApplicationService.Implementation
{
    public class OrderServiceTest
    {
        #region MockData

        class OrderTestData : IEnumerable<Object[]>
        {
            readonly Order o1 = new Order() {
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { ID = 1 },
                OrderDate = DateTime.Now,
                AtAddress = true,
                Services = "TestService",
                IsApproved = false
            };

            readonly Order o2 = new Order() {
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { ID = 1 },
                OrderDate = DateTime.Now.AddMonths(-5),
                AtAddress = false,
                Services = "TestService TestService",
                IsApproved = false
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { o1 };
                yield return new object[] { o2 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        #endregion

        #region CreateOrderTests

        [Theory]
        [ClassData(typeof(OrderTestData))]
        public void OrderAddTest(Order order)
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            orderService.AddOrder(order);
            moqRep.Verify(x => x.Create(order), Times.Once);
        }

        #endregion

        #region ApproveOrderTest

        [Theory]
        [ClassData(typeof(OrderTestData))]
        public void OrderApproveTest(Order order)
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            order.ID = 1;

            moqRep.Setup(x => x.ReadByID(order.ID)).Returns(order);

            orderService.ApproveOrder(order);
            moqRep.Verify(x => x.Update(order), Times.Once);
        }

        #endregion

        #region DeleteTest

        [Theory]
        [ClassData(typeof(OrderTestData))]
        public void OrderDeleteTest(Order order)
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            order.ID = 1;

            orderService.DeleteOrder(1);
            moqRep.Verify(x => x.Delete(1), Times.Once);
        }

        #endregion

        #region UpdateOrderTest

        [Theory]
        [ClassData(typeof(OrderTestData))]
        public void OrderUpdateTest(Order order)
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            order.ID = 1;

            orderService.UpdateOrder(order);
            moqRep.Verify(x => x.Update(order), Times.Once);
        }

        #endregion

    }
}
