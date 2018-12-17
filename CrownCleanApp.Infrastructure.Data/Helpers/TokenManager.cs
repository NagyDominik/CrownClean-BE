using CrownCleanApp.Core.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrownCleanApp.Infrastructure.Data.Managers
{
    public class TokenManager
    {
        private readonly byte[] _jwtKey;
        private readonly double _JwtExpireDays;
        private readonly string _jwtIssuer;


        public TokenManager(string jwtKey, double JwtExpireDays, string jwtIssuer)
        {
            this._jwtKey = Encoding.UTF8.GetBytes(jwtKey);
            this._JwtExpireDays = JwtExpireDays;
            this._jwtIssuer = jwtIssuer;
        }

        public TokenManager(byte[] jwtKey, double JwtExpireDays, string jwtIssuer)
        {
            this._jwtKey = jwtKey;
            this._JwtExpireDays = JwtExpireDays;
            this._jwtIssuer = jwtIssuer;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("emailAddress", user.Email),
                new Claim("id", user.ID.ToString())
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim("role", "Administrator"));
            }
            else
            {
                claims.Add(new Claim("role", "User"));
            }

            var key = new SymmetricSecurityKey(_jwtKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_JwtExpireDays);

            var token = new JwtSecurityToken(
                _jwtIssuer,
                _jwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
