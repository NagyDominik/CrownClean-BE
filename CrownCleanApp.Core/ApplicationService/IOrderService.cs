using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.ApplicationService
{
    public interface IOrderService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order AddOrder(Order order);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Order> GetAllOrders();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order GetOrderByID(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order UpdateOrder(Order order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order ApproveOrder(Order order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order DeleteOrder(int id);
    }
}
