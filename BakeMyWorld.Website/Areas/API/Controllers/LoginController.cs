using BakeMyWorld.Website.Areas.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeMyWorld.Website.Areas.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        // POST api/login
        public IActionResult Login(AdminDto login)
        {
            IActionResult response = Unauthorized();
            var admin = AuthenticateAdmin(login);

            if (admin != null)
            {
                var tokenString = GenerateJSONWebToken(admin);
                var tokenDto = new TokenDto { Value = tokenString};
                response = Ok(tokenDto);
            }

            return response;
        }

        private string GenerateJSONWebToken(AdminDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private AdminDto AuthenticateAdmin(AdminDto login)
        {
            AdminDto admin = null;
   
            if (login.AdminName == "Admin" && login.AdminPassword == "123")
            {
                admin = new AdminDto { AdminName = "Admin", AdminPassword = "123" };
            }
            return admin;
        }
    }
}
