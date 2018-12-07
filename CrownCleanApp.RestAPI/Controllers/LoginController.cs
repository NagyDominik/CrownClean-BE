﻿using System;
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
    [Route("api/[controller]")]
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
                byte[] pwHash, pwSalt;

                _authenticationHelper.CreatePasswordHash(dto.Password, out pwHash, out pwSalt);

                var user = new User()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    IsCompany = dto.IsCompany,
                    PhoneNumber = dto.PhoneNumber,
                    Addresses = dto.Addresses,
                    PasswordHash = pwHash,
                    PasswordSalt = pwSalt
                };

                User userFound = _userService.AddUser(user);

                return Ok(_tokenManager.GenerateJwtToken(userFound));
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}