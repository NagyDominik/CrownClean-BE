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
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public ActionResult<FilteredList<User>> Get([FromQuery] UserFilter filter)
        {
            try
            {
                if (!string.IsNullOrEmpty(filter.Name) || !string.IsNullOrEmpty(filter.Email) || filter.ItemsPerPage > 0)
                {
                    return Ok(_userService.GetAllUsers(filter));
                }
                else
                {
                    return Ok(_userService.GetAllUsers(null));
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<User> Get(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("ID must be bigger than 0!");
                }

                // Administrators can access the details of other users
                if (!string.Equals(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value, "Administrator"))
                {
                    // Retrieve the id of the user, as stored in the JWT
                    var userIDFromAuth = HttpContext.User.Claims.FirstOrDefault(n => n.Type == "id").Value;

                    int.TryParse(userIDFromAuth, out int userID);

                    // Check if the user is trying to access the details of another user using its own walid JWT.
                    if (userID != id)
                    {
                        return Forbid();
                    }
                }
                

                User user = _userService.GetUserByID(id);

                if (user == null)
                {
                    return BadRequest($"User with the ID of {id} was not found!");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Users
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                if (user.ID != 0)
                {
                    return BadRequest("Cannot add a User with already existing ID!");
                }

                User tmp = _userService.AddUser(user);
             
                if (tmp == null)
                {
                    return BadRequest("Could not add User!");
                }
                else
                {
                    string s = String.Format($"A new user with the ID of {tmp.ID} hass been added.");
                    return Ok(s);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<User> Put([FromBody] User user)
        {
            try
            {
                User tmp = _userService.GetUserByID(user.ID);

                if (tmp == null)
                {
                    return BadRequest("Could not found User!");
                }


                // Administrators can update the details of any user
                if (!string.Equals(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value, "Administrator"))
                {
                    // Retrieve the id of the user, as stored in the JWT
                    var userIDFromAuth = HttpContext.User.Claims.FirstOrDefault(n => n.Type == "id").Value;

                    int.TryParse(userIDFromAuth, out int userID);

                    // Check if the user is trying to access the details of another user using its own walid JWT.
                    if (userID != user.ID)
                    {
                        return Forbid();
                    }
                }

                tmp = _userService.UpdateUser(user);

                if (tmp == null)
                {
                    return BadRequest("Could not found User!");
                }
                else
                {

                    String s = String.Format($"The user with ID of {tmp.ID} was updated!");
                    return Ok(s);
                }


            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<User> Delete(int id)
        {
            try
            {
                var a = HttpContext.Items;

                if (id < 1)
                {
                    return BadRequest("Cannot delete non-existing user!");
                }

                User tmp = _userService.DeleteUser(id);

                if (tmp == null)
                {
                    return BadRequest("Could not delete User!");
                }
                else
                {
                    String s = String.Format($"The user with ID of {tmp.ID} was deleted!");
                    return Ok(s);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<Order> Approve(int id)
        {
            if (id == 0)
                BadRequest("Order ID must be provided!");
            try
            {
                return Ok(_userService.ApproveUser(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
