using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TopStyle.Domain.Auth.Interface;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;

namespace TopStyle.Domain.Auth.Authentication
{
    public class JwtTokenService:IJwtTokenService
    {
         string IJwtTokenService.CreateToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#123456789101112"));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                   issuer: "http://localhost:5231/",
                   audience: "http://localhost:5231/",
            claims: claims,
                   expires: DateTime.Now.AddMinutes(20),
                   signingCredentials: signInCredentials

                   );
            //Här genereras en token upp enligt den konfiguration som vi satt 
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
    }
}
