#nullable disable
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using To_Do_List.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Bugshield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Context _context;
        public AuthController(Context context)
        {
            _context = context;
        }
        /// <summary>
        /// To  Login  and send Response to Client side
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [Route("Users")]
        [HttpPost]
        public IActionResult EmployeeLogin(LoginModel loginModel)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == loginModel.Email && x.Password == loginModel.Password);

            if (user == null)
            {
                return BadRequest(new Response
                {
                    Status = "Invalid",
                    Message = "Invalid User.",
                    Email = "Invalid",
                    Username = "Invalid",
                    Phone = "Invalid",
                    Token = "Invalid",
                    Id = 0,
                });
            }
            else
            {
                IActionResult tokenResult = GetToken(user);

                if (tokenResult is OkObjectResult okObjectResult)
                {
                    string jwtToken = okObjectResult.Value?.ToString();
                    var response = new Response
                    {
                        Status = "Success",
                        Message = "Login Successfully",
                        Email = user.Email,
                        Username = user.Name,
                        Phone = user.Phone,
                        Token = jwtToken,
                        Id = user.Id,
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Failed to generate token.");
                }
            }
        }
        /// <summary>
        /// To generate Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("gettoken")]
        public IActionResult GetToken(User user)
        {
            int id = user.Id;
            var userRole = "no role";
            if (id != 0)
            {
                 userRole = "User";
            }

            var key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Mih4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx";
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userRole),
            };

            var token = new JwtSecurityToken(
                issuer: "JWTAuthenticationServer",
                audience: "JWTServicePostmanClient",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwtToken);
        }

    }
}




