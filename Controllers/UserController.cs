using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using backend.Services;
using BCrypt.Net;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly string jwtSecret = "mysecurekey";
        private readonly int jwtExpirationMinutes = 60;
        private readonly AppDbContext context;
        private readonly IUserRepository userRepository;

        public UserController(IUserService userService, AppDbContext context, IUserRepository userRepository)
        {
            this.userService = userService;
            this.context = context;
            this.userRepository = userRepository;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {
            var user = userService.Register(model);

            if (user == null)
            {
                return BadRequest("Registration failed.");
            }

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            var user = userService.Login(model);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        [HttpPut("userpreferences/{userEmail}")]
        public IActionResult UpdateUserPreferences(string userEmail, [FromBody] UserPreference updatedPreference)
        {
            var user = userRepository.GetUserByEmail(userEmail);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var userPreferences = context.UserPreferences.FirstOrDefault(up => up.UserId == user.Id);

            if (userPreferences == null)
            {
                return NotFound("User preferences not found");
            }

            userPreferences.ShowMovies = updatedPreference.ShowMovies;
            userPreferences.ShowTVShows = updatedPreference.ShowTVShows;

            context.SaveChanges();

            return Ok("User preferences updated successfully");
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyLongSecretKeyForJwtTokenGenerationThatIhaveForNowUseIt"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(jwtExpirationMinutes),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
