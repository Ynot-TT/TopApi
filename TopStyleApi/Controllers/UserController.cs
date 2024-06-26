﻿using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopStyle.Core.Interfaces;
using TopStyle.Domain.Auth.Authentication;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TopStyleApi.Domain.DTO;
using TopStyle.Domain.Auth.Interface;
using TopStyleApi.Core.Interfaces;

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
        private readonly IOrderService _orderService;

        public UserController(IMapper mapper, IUserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtTokenService jwtTokenService, IOrderService orderService)
        {
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _orderService = orderService;
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register( UserDTO userDTO)
        {
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

            var userDetails = new
            {
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
            var orders = await _orderService.GetOrdersByUserIdAsync(user.Id);

            return Ok(new { Message = "Logged In", Token = token, UserDetails = userDetails, Orders = orders });
        }

        
    }
}