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
        private readonly string _jwtKey;
        private readonly double _jwtExpireDays;
        private readonly string _jwtIssuer;


        public TokenManager(string jwtKey, double jwtExpireDays, string jwtIssuer)
        {
            this._jwtKey = jwtKey;
            this._jwtExpireDays = jwtExpireDays;
            this._jwtIssuer = jwtIssuer;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("emailAddress", user.Email),
                new Claim("id", user.ID.ToString())
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtExpireDays);

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
