using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthJWT
{
    public interface IJWTService
    {
        string GenerateToken(List<Claim> claims);
    }

    public class JWTService : IJWTService
    {
        private readonly AppSettings _appSettings;

        public JWTService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(List<Claim> claims)
        {
            var now = DateTime.UtcNow;
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "authJWT",
                audience: "authJWT",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(30),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
