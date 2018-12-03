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
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            try
            {
                return Ok(_userService.GetAllUsers());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("ID must be bigger than 0!");
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
        public ActionResult<User> Put([FromBody] User user)
        {
            try
            {
                User tmp = _userService.UpdateUser(user);

                if (tmp == null)
                {
                    return BadRequest("Could not update User!");
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
        public ActionResult<User> Delete(int id)
        {
            try
            {
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
    }
}
