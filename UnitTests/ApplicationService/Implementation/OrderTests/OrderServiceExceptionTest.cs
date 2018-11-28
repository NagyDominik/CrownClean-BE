using System;
using Xunit;
using Moq;
using CrownCleanApp.Core.DomainService;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.ApplicationService.Services;
using CrownCleanApp.Core.Entity;
using System.IO;

namespace TestCore.ApplicationService.Implementation
{
    public class OrderServiceExceptionTest
    {

        #region OrderAddTests

        [Fact]
        public void AddOrderWithIDThrowsException()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() { ID = 1 };
            Order newOrder2 = new Order() { ID = -1 };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder));
            Exception e2 = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder2));
            Assert.Equal("Cannot add order with ID!", e.Message);
            Assert.Equal("Cannot add order with ID!", e2.Message);
        }

        [Fact]
        public void AddOrderWithoutUserThrowsException()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() { User = new User() { }, Vehicle = new Vehicle() { ID = 1 } };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder));
            Assert.Equal("Cannot add order without user!", e.Message);
        }

        [Fact]
        public void AddOrderWithoutVehicleThrowsException()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() {
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { }
            };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder));
            Assert.Equal("Cannot add order without vehicle!", e.Message);
        }

        [Fact]
        public void AddOrderWithoutServiceThrowsException()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() {
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { ID = 7 },
                Services = ""
            };

            Order newOrder2 = new Order() {
                User = new User() { ID = 1 },
                Vehicle = new Vehicle() { ID = 7 },
                Services = null
            };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder));
            Exception e2 = Assert.Throws<InvalidDataException>(() => orderService.AddOrder(newOrder2));
            Assert.Equal("Cannot add order without service!", e.Message);
            Assert.Equal("Cannot add order without service!", e2.Message);
        }

        #endregion

        #region OrderApproveTests

        [Fact]
        public void ApproveOrderWithNullOrder()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() { ID = 0 };
            Order newOrder2 = null;

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.ApproveOrder(newOrder));
            Exception e2 = Assert.Throws<InvalidDataException>(() => orderService.ApproveOrder(newOrder2));
            Assert.Equal("Cannot approve order without ID!", e.Message);
            Assert.Equal("Cannot approve order without ID!", e2.Message);
        }

        [Fact]
        public void ApproveOrderWithisApprovedTrue()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() { ID = 1, IsApproved = true };

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.ApproveOrder(newOrder));
            Assert.Equal("Cannot approve order with approved status!", e.Message);
        }

        #endregion

        #region OrderDeleteTests

        [Fact]
        public void DeleteOrderWithoutID()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.DeleteOrder(0));
            Assert.Equal("Cannot delete order without ID!", e.Message);
        }

        #endregion

        #region OrderGetByIDTests

        [Fact]
        public void GetOrderByIDWithoutID()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.GetOrderByID(0));
            Assert.Equal("Cannot get order by ID without ID!", e.Message);
        }

        #endregion

        #region OrderUpdateTests

        [Fact]
        public void OrderUpdateWithoutID()
        {
            var moqRep = new Mock<IOrderRepository>();
            IOrderService orderService = new OrderService(moqRep.Object);

            Order newOrder = new Order() { ID = 0 };
            Order newOrder2 = null;

            Exception e = Assert.Throws<InvalidDataException>(() => orderService.UpdateOrder(newOrder));
            Exception e2 = Assert.Throws<InvalidDataException>(() => orderService.UpdateOrder(newOrder2));
            Assert.Equal("Cannot update order without ID!", e.Message);
            Assert.Equal("Cannot update order without ID!", e2.Message);
        }

        #endregion

    }
}
