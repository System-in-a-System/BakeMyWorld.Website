using BakeMyWorld.Website.Areas.API.Models;
using BakeMyWorld.Website.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BakeMyWorld.Website.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;

        public LoginController(IConfiguration config, UserManager<User> userManager)
        {
            this.config = config;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        // POST api/login
        public IActionResult Login(CredentialsDto credentialsDto)
        {
            var user = userManager.FindByNameAsync(credentialsDto.Nickname).GetAwaiter().GetResult();
            var hasAccess = user.Password == credentialsDto.Password ? true : false;

            if (hasAccess)
            {
                var token = GenerateJSONWebToken(user);
                return Ok(token);
            }

            return Unauthorized(); // 401 Unauthorized         
        }

        private TokenDto GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            var roles = userManager.GetRolesAsync(user).Result;

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //claims.Add(new Claim(JwtRegisteredClaimNames.Sub, customer.UserName))
            claims.Add(new Claim("Nickname", user.Nickname));
            claims.Add(new Claim("Email", user.Email));
            

            var tokenDescription = new JwtSecurityToken(config["Jwt:Issuer"],
              config["Jwt:Issuer"],
              claims: claims,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescription);

            var tokenDto = new TokenDto { Value = token };

            return tokenDto;
        }
    }
}
