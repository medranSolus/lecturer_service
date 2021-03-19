using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LecturerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        readonly IConfiguration config;
        readonly ILogger<LoginController> logger;

        public LoginController(IConfiguration configuration, ILogger<LoginController> log)
        {
            config = configuration;
            logger = log;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]Data.User login)
        {
            Data.User user = AuthenticateUser(login);
            if (user != null)
                return Ok(new { token = GenerateJWT(user) });
            return Unauthorized();
        }

        Data.User AuthenticateUser(Data.User login)
        {
            Data.User user = null;
            // TOD: DB etc
            if (login.Login == "TEST")
            {
                user = new Data.User
                {
                    Login = "TEST",
                    Password = "TESTO"
                };
            }
            return user;
        }

        string GenerateJWT(Data.User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Login)
            };

            var token = new JwtSecurityToken(config["JWT:Issuer"],
                config["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}