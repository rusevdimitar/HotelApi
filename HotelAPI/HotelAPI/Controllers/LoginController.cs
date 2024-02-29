// using Contracts;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        // private readonly ILoggerManager _logger;

        public LoginController(IConfiguration config /*, ILoggerManager logger */)
        {
            _config = config;
            // _logger = logger;
        }

        // TODO: DIR: Use the actual reqeuest
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            // TEST
            //_logger.LogInfo("Here is info message from the controller.");
            //_logger.LogDebug("Here is debug message from the controller.");
            //_logger.LogWarn("Here is warn message from the controller.");
            //_logger.LogError("Here is error message from the controller.");

            //If login usrename and password are correct then proceed to generate token
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(token);
        }
    }
}
