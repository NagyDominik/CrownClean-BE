﻿using CrownCleanApp.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrownCleanApp.Core.ApplicationService
{
    public interface IVehicleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        Vehicle AddVehicle(Vehicle vehicle);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Vehicle> GetAllVehicles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Vehicle GetVehicleByID(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        Vehicle UpdateVehicle(Vehicle vehicle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Vehicle DeleteVehicle(int id);
    }
}
