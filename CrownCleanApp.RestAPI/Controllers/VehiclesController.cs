using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrownCleanApp.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            this._vehicleService = vehicleService;
        }

        // GET: api/Vehicles
        [HttpGet]
        [Authorize]
        public ActionResult<FilteredList<Vehicle>> Get([FromQuery] VehicleFilter filter)
        {
            try
            {

                if (!string.IsNullOrEmpty(filter.Brand) || !string.IsNullOrEmpty(filter.Type) || !string.IsNullOrEmpty(filter.UniqueID) || filter.FilterSize || filter.ItemsPerPage > 0)
                {
                    if (filter.UserID > 0)
                    {
                        return Ok(_vehicleService.GetVehiclesOfACustomer(filter, filter.UserID));
                    }

                    return Ok(_vehicleService.GetAllVehicles(filter));
                }
                else
                {
                    if (filter.UserID > 0)
                    {
                        return Ok(_vehicleService.GetVehiclesOfACustomer(filter, filter.UserID));
                    }

                    return Ok(_vehicleService.GetAllVehicles(null));
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Vehicle> Get(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("ID must be bigger than 0!");
                }

                Vehicle vehicle = this._vehicleService.GetVehicleByID(id);

                // Administrators can access the details of all vehicles
                if (!string.Equals(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value, "Administrator"))
                {
                    // Retrieve the id of the user, as stored in the JWT
                    var userIDFromAuth = HttpContext.User.Claims.FirstOrDefault(n => n.Type == "id").Value;

                    int.TryParse(userIDFromAuth, out int userID);

                    // Check if the user is trying to access another user's order
                    if (vehicle.User.ID != id)
                    {
                        return Forbid();
                    }
                }


                if (vehicle == null)
                {
                    return BadRequest($"Could not find vehicle with the id of {id}");
                }
                else
                {
                    return vehicle;
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Vehicles
        [HttpPost]
        [Authorize]
        public ActionResult<Vehicle> Post([FromBody] Vehicle vehicle)
        {
            try
            {
                if (vehicle.ID != 0)
                {
                    return BadRequest("Cannot add vehicle with ID!");
                }

                Vehicle tmp = this._vehicleService.AddVehicle(vehicle);

                if (tmp == null)
                {
                    return BadRequest("Could not add vehicle");
                }
                else
                {
                    string s = String.Format($"A new vehicle with the ID of {tmp.ID} hass been added.");
                    return Ok(s);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Vehicle> Put([FromBody] Vehicle vehicle)
        {
            try
            {
                Vehicle tmp = this._vehicleService.UpdateVehicle(vehicle);

                if (tmp == null)
                {
                    return BadRequest("Could not update vehicle!");
                }
                else
                {
                    String s = String.Format($"The vehicle with ID of {tmp.ID} was updated!");
                    return Ok(s);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Vehicle> Delete(int id)
        {
            try
            {

                if (id < 1)
                {
                    return BadRequest("Cannot delete non-existing vehicle!");
                }

                Vehicle tmp = this._vehicleService.GetVehicleByID(id);


                // Administrators can delete any vehicle
                if (!string.Equals(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value, "Administrator"))
                {
                    // Retrieve the id of the user, as stored in the JWT
                    var userIDFromAuth = HttpContext.User.Claims.FirstOrDefault(n => n.Type == "id").Value;

                    int.TryParse(userIDFromAuth, out int userID);

                    // Check if the user is trying to delete someone else's vehicle
                    if (userID != tmp.User.ID)
                    {
                        return Forbid();
                    }
                }

                tmp = this._vehicleService.DeleteVehicle(id);

                if (tmp == null)
                {
                    return BadRequest("Could not delete vehicle!");
                }
                else
                {
                    String s = String.Format($"The vehicle with ID of {tmp.ID} was deleted!");
                    return Ok(s);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
