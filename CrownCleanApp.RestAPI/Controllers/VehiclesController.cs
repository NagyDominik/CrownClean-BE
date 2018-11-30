using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrownCleanApp.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _service;

        public VehiclesController(IVehicleService orderService)
        {
            _service = orderService;
        }

        // GET: api/Vehicles
        [HttpGet]
        public ActionResult<List<Vehicle>> Get()
        {
            try
            {
                return Ok(_service.GetAllVehicles());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            if (id <= 0)
            {
                BadRequest("ID must be greater than 0!");
            }

            try
            {
                return Ok(_service.GetVehicleByID(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Vehicles
        [HttpPost]
        public ActionResult<Vehicle> Post([FromBody] Vehicle vehicle)
        {
            if(vehicle.ID != 0)
                BadRequest("Vehicle ID must be 0!");
            if (vehicle.User == null)
                BadRequest("User must be provided!");

            try
            {
                return Ok(_service.AddVehicle(vehicle));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public ActionResult<Vehicle> Put(int id, [FromBody] Vehicle vehicle)
        {
            if (vehicle.ID == 0)
                BadRequest("Vehicle ID must be provided!");

            try
            {
                return Ok(_service.UpdateVehicle(vehicle));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Vehicle> Delete(int id)
        {
            if (id == 0)
                BadRequest("Vehicle ID must be provided!");

            try
            {
                return Ok(_service.DeleteVehicle(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
