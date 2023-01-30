using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TokenAuthMVCCoreApp.Models;

namespace TokenAuthMVCCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            if (user.Username.Equals("admin") && user.Password.Equals("admin"))
            {
                //unique guid
                user.Id = Convert.ToInt32(Guid.NewGuid());

                //calls another method
                var token = GenerateJwtToken(user);
                return Ok(token);
            }
            return BadRequest("Invaled Username or Password");
        }


        //generate JWT token method
        private string GenerateJwtToken(User user)
        {
            //create a secret known to client :present in Jwt section in appsetting.json
            var securityKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);

            //put some claim data
            var claims = new Claim[] {
                 new Claim(ClaimTypes.Name, , user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.Username)
            };

            //create hash key using encryption algorithm
            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);

            //payload data
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
             claims,
             expires: DateTime.Now.AddDays(7),
             signingCredentials: credentials);

            //write token
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }


}
