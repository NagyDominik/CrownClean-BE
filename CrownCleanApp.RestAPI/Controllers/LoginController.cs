using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrownCleanApp.Core.ApplicationService;
using CrownCleanApp.Core.Entity;
using CrownCleanApp.Infrastructure.Data.Managers;
using CrownCleanApp.RestAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CrownCleanApp.RestAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenManager _tokenManager;
        private readonly IAuthenticationHelper _authenticationHelper;

        public LoginController(IUserService userService, IConfiguration configuration, IAuthenticationHelper authenticationHelper)
        {
            this._userService = userService;
            this._authenticationHelper = authenticationHelper;
            this._tokenManager = new TokenManager(
                configuration["JwtKey"],
                double.Parse(configuration["JwtExpireDays"]),
                configuration["JwtIssuer"]
            );
        }

        [HttpPost]
        public ActionResult<string> Register([FromBody] RegisterDTO dto)
        {
            try
            {
                _authenticationHelper.CreatePasswordHash(dto.Password, out byte[] pwHash, out byte[] pwSalt);

                var user = new User()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    IsCompany = dto.IsCompany,
                    PhoneNumber = dto.PhoneNumber,
                    Addresses = new List<string>() { dto.Address},
                    PasswordHash = pwHash,
                    PasswordSalt = pwSalt
                };

                User userFound = _userService.AddUser(user);

                //return Ok(new { token = _tokenManager.GenerateJwtToken(userFound)});
                return Ok("USER_ADDED");
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public ActionResult<string> Login([FromBody] LoginDTO dto)
        {
            try
            {
                User user = _userService.GetAllUsers(null).List.FirstOrDefault(u => u.Email == dto.Email);

                if (user == null)
                {
                    return Unauthorized();
                }

                if (!user.IsApproved)
                {
                    return Unauthorized();
                }

                if (!_authenticationHelper.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return Unauthorized();
                }

                // For now only return the token.
                return Ok(new { token = _tokenManager.GenerateJwtToken(user) });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}