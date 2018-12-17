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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService orderService)
        {
            _service = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult<FilteredList<Order>> Get([FromQuery] OrderFilter filter)
        {
            try
            {
                if (!string.IsNullOrEmpty(filter.ServicesSearch) || !string.IsNullOrEmpty(filter.DescriptionSearch) || filter.UserID > 0 || filter.ItemsPerPage > 0)
                {
                    if (filter.UserID > 0)
                    {
                        Ok(_service.GetOrdersOfACustomer(filter, filter.UserID));
                    }

                    return Ok(_service.GetAllOrders(filter));
                }
                else
                {
                    if (filter.UserID > 0)
                    {
                        Ok(_service.GetOrdersOfACustomer(null, filter.UserID));
                    }

                    return Ok(_service.GetAllOrders(null));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Order> Get(int id)
        {
            if (id <= 0) {
                BadRequest("ID must be greater than 0!");
            }
            try {

                Order order = _service.GetOrderByID(id);

                
                // Administrators can access the details of all orders
                if (!string.Equals(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value, "Administrator"))
                {
                    // Retrieve the id of the user, as stored in the JWT
                    var userIDFromAuth = HttpContext.User.Claims.FirstOrDefault(n => n.Type == "id").Value;

                    int.TryParse(userIDFromAuth, out int userID);

                    // Check if the user is trying to access another user's order
                    if (order.UserID != id)
                    {
                        return Forbid();
                    }
                }

                if (order == null)
                {
                    return BadRequest($"Could not find the order with the id of {id}");
                }

                return Ok(order);
            
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Orders
        [HttpPost]
        [Authorize]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            if (order.ID != 0)
                BadRequest("Order ID must be 0!");
            if (order.User == null || order.Vehicle == null)
                BadRequest("User and Vehicle must be provided!");

            try {
                return Ok(_service.AddOrder(order));
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (order.ID == 0)
                BadRequest("Order ID must be provided!");

            try {
                return Ok(_service.UpdateOrder(order));
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Order> Delete(int id)
        {
            if (id == 0)
                BadRequest("Order ID must be provided!");

            try {
                return Ok(_service.DeleteOrder(id));
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<Order> Approve(int id)
        {
            if (id == 0)
                BadRequest("Order ID must be provided!");
            try {
                return Ok(_service.ApproveOrder(id));
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
