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
        readonly Model.LSContext dbCtx;
        readonly ILogger<LoginController> logger;

        public LoginController(IConfiguration configuration, Model.LSContext database, ILogger<LoginController> log)
        {
            config = configuration;
            dbCtx = database;
            logger = log;
        }
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]Data.User login)
        {
            if (AuthenticateUser(login))
                return Ok(new { token = GenerateJWT(login) });
            return Unauthorized();
        }

        bool AuthenticateUser(Data.User login)
        {
            Model.Password pass = dbCtx.Passwords.Find(login.Login);
            if (pass != null)
                return pass.Pass == Data.Security.GetHash(login.Password);
            return false;
        }

        string GenerateJWT(Data.User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(Data.Security.ClaimLoginID, user.Login)
            };

            var token = new JwtSecurityToken(issuer: config["JWT:Issuer"],
                audience: config["JWT:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}