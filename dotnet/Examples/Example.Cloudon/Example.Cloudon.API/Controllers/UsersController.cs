using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Example.Cloudon.API.Dtos;
using Example.Cloudon.API.Helpers;
using Example.Cloudon.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Example.Cloudon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UsersController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> AuthenticateUser(UserLoginAndRegisterDto user)
        {
            if (user.Username == null || user.Password == null)
            {
                return BadRequest(new { message = "Username or password is empty!" });
            }
            
            var hashedPassword = StringHelper.ToSha256String(user.Password);
            var userExist = await _userRepository.AnyAsync(user.Username, hashedPassword);
            if (!userExist)
            {
                return BadRequest(new { message = "Username or password is wrong. Try again!" });
            }

            var token = GenerateToken(user.Username);
            return Ok(token);
        }

        private string GenerateToken(string username)
        {
            var jwtSecret = _configuration[Consts.ConfigurationKeys.Jwt.Secret];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, username) };
            var token = new JwtSecurityToken
            (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(7),

                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterUser(UserLoginAndRegisterDto user)
        {
            if (user.Username == null || user.Password == null) return BadRequest(new { message = "Username or password is empty!" });
            
            var userExist = await _userRepository.AnyAsync(user.Username);
            if (userExist) return BadRequest(new { message = "Username already exist!" });

            var hashedPassword = StringHelper.ToSha256String(user.Password);
            var userSaved = await _userRepository.AddAsync(user.Username, hashedPassword);
            if (!userSaved) return BadRequest(new { message = "Oops! Something unexpected happen. Please try again." });

            var token = await AuthenticateUser(user);
            return token;
        }

        // TODO: Delthen
        [HttpPost("auth")]
        [Authorize]
        public IActionResult Auth()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null) return null;

            var userClaims = identity.Claims;
            var userName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok($"{userName}");
        }
    }
}
