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
using Microsoft.EntityFrameworkCore;
using TopStyleApi.Domain.DTO;

namespace TopStyle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;

        public UserController(IMapper mapper, IUserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtTokenService jwtTokenService)
        {
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register( UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }

            var userExists = await _userManager.FindByNameAsync(userDTO.Username);
            if (userExists != null)
            {
                return BadRequest("UserName already exists");
            }

            var user = _mapper.Map<ApplicationUser>(userDTO);    
            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }
            return Ok("User sucessfully registered");
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] UserLogInDTO userDTO)
        {
            var user = await _userManager.FindByNameAsync(userDTO.Username);

            var result = await _signInManager.CheckPasswordSignInAsync(user!, userDTO.Password, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                return Unauthorized("Wrong username or password");
            }

            var token = _jwtTokenService.CreateToken(user!.Id);

            // Fetch additional user details
            var userDetails = new
            {
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

            return Ok(new { Message = "Logged In", Token = token, UserDetails = userDetails });
        }

        [HttpGet]
        [Route("/api/all-users")]
        public async Task<ActionResult<UserDTO>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        //[HttpGet]
        //[Route("/api/user/{id}")]
        //public async Task<ActionResult<UserDTO>> GetUser(string id)
        //{
        //    var user = await _userService.GetUserByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound("User not found");
        //    }
        //    return Ok(user);
        //}

        //[HttpPut]
        //[Route("/api/user/{id}")]
        //public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] UserDTO userDTO)
        //{
        //    //if (id != userDTO.UserId)
        //    //{
        //    //    return BadRequest("User ID mismatch.");
        //    //}

        //    //await _userService.UpdateUserAsync(userDTO);

        //    //if (userDTO == null)
        //    //{
        //    //    return NotFound("User not found");
        //    //}
        //    return Ok(userDTO);
        //}

        //[HttpDelete]
        //[Route("/api/user/{id}")]
        //public async Task<ActionResult> DeleteUser(string id)
        //{
        //    await _userService.DeleteUserAsync(id);
        //    return Ok("User deleted");
        //}
    }
}