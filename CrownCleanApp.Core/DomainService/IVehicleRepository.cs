using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.DomainService
{
    public interface IVehicleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Vehicle Create(Vehicle vehicle);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Vehicle> ReadAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Vehicle ReadByID(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        Vehicle Update(Vehicle vehicle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Vehicle Delete(int id);
    }
}
