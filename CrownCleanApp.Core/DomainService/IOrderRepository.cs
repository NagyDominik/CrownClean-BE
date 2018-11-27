using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService
{
    public interface IOrderRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Order Create(Order order);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Order> ReadAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order ReadByID(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order Update(Order order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Order Delete(int id);
    }
}
