using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NEWZEAL_LAND_WORK_API.Repositories;
using NEWZEAL_LAND_WORK_API.UsersLoging;

namespace NEWZEAL_LAND_WORK_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly ITokenRepository _tokenRepository;
        
        public AuthController(UserManager<IdentityUser> userManager, 
            ITokenRepository tokenRepository,
            ILogger <AuthController> logger)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
            this._logger = logger;
            
        }

        [HttpPost] // Changed to HttpPost as it's more appropriate for creating resources
        [Route("Api/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                return BadRequest("User data is null.");
            }

            var identityUser = new IdentityUser
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Username,
            };

            var result = await _userManager.CreateAsync(identityUser, registerUserDto.password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (registerUserDto.Roles != null && registerUserDto.Roles.Any())
            {
                foreach (var role in registerUserDto.Roles)
                {
                    var roleResult = await _userManager.AddToRoleAsync(identityUser, role);
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }
            }

            return Ok(new { UserId = identityUser.Id, Message = "User registered successfully." });
        }



        [HttpPost]
        [Route("Api/Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("Login data is null.");
            }

            // Check if the user exists by email
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email.");
            }

            // Verify the password
            var passwordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!passwordValid)
            {
                return Unauthorized("Invalid password.");
            }

            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);
            if (roles != null)
            {
                // Generate a JWT token (or use another authentication mechanism)
                var token = _tokenRepository.CreateJwtToken(user, roles.ToList());

                // Return the token and user details
                return Ok(new
                {
                    Token = token,
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return BadRequest("Username and Password is incorrect");

        }

    }
}