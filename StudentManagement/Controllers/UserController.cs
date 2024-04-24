using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystum.Models;
using StudentManagementSystum.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagementSystum.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserController(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                // Check if the username is unique
                var isUniqueUser = _userRepository.IsUniqueUser(user.Email);
                if (!isUniqueUser)
                {
                    return BadRequest("User is already in use.");
                }
                var registeredUser = _userRepository.Register(user.Email, user.Password);
                if (registeredUser != null)
                {
                    return Ok(registeredUser);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to register user.");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserVM userVM)
        {
            try
            {
                var user = _userRepository.Authenticate(userVM.Email, userVM.Password);
                if (user == null)
                {
                    return BadRequest("Incorrect email or password.");
                }

                // JWT Token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


    }
}
