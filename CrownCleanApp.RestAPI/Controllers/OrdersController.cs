using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.DomainService.Filtering;
using CrownCleanApp.Core.Entity;
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
        public ActionResult<FilteredList<Order>> Get([FromQuery] OrderFilter filter)
        {
            try
            {
                if (!string.IsNullOrEmpty(filter.ServicesSearch) || !string.IsNullOrEmpty(filter.DescriptionSearch) || filter.UserID > 0 || filter.ItemsPerPage > 0)
                {
                    return Ok(_service.GetAllOrders(filter));
                }
                else
                {
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
        public ActionResult<Order> Get(int id)
        {
            if (id <= 0) {
                BadRequest("ID must be greater than 0!");
            }
            try {
                return Ok(_service.GetOrderByID(id));
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Orders
        [HttpPost]
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
