using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopStyle.Core.Interfaces;
using TopStyle.Domain.Auth.Authentication;
using TopStyle.Domain.Auth.Interface;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;
using System.Security.Claims;

namespace TopStyle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data");
            }
            var userModel = new ApplicationUser
            {
                UserName = user.Username

            };
            var result = await _userManager.CreateAsync(userModel, user.Password);

            if (result.Succeeded)
            {
                await _userService.AddUserAsync(user);
                return Ok("User created");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);

            string token = _jwtTokenService.CreateToken(user);
            if (result.Succeeded)
            {
                return Ok($"Inloggad \nToken: {token}"); 
            }
            else
            {
                return Unauthorized("Wrong username or password");
            }
        }

        [HttpGet]
        [Route("/api/all-users")]
        public async Task<ActionResult<UserDTO>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("/api/user/{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPut]
        [Route("/api/user/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            if (id != userDTO.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            await _userService.UpdateUserAsync(userDTO);

            if (userDTO == null)
            {
                return NotFound("User not found");
            }
            return Ok(userDTO);
        }

        [HttpDelete]
        [Route("/api/user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok("User deleted");
        }

        [HttpGet]
        public async Task<ActionResult> GetLoggedInUsers()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                if (Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    // User ID parsed successfully
                    return Ok(new { UserID = userId });
                }
                else
                {
                    // Unable to parse user ID
                    return BadRequest("User ID could not be parsed");
                }
            }
            else
            {
                // User ID claim not found in token
                return BadRequest("User ID claim not found in token");
            }

        }
    }
}