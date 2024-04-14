using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopStyle.Domain.Entities;
using TopStyleApi.Core.Interfaces;
using TopStyleApi.Core.Services;
using TopStyleApi.Domain.DTO;
using TopStyleApi.Domain.Entities;

namespace TopStyleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(AddOrderDTO addOrderDTO)
        {
            //await _orderService.AddOrderAsync(addOrderDTO);
            //return Ok("order created");

            if (addOrderDTO == null || addOrderDTO.Items == null || !addOrderDTO.Items.Any())
            {
                return BadRequest("Invalid order data");
            }

            // Get the user ID from the token
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the user ID is valid
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User not authenticated");
            }

            // Parse the user ID claim to a Guid
            if (!Guid.TryParse(userIdClaim, out Guid userIdGuid))
            {
                return BadRequest("Invalid user ID format");
            }

            // Convert the Guid to an int (assuming you want to store it as an int)
            int userId = userIdGuid.GetHashCode();

            // Assign the user ID to the AddOrderDTO
            addOrderDTO.UserId = userId;

            // Save the order to the database
            await _orderService.AddOrderAsync(addOrderDTO);

            return Ok("Order created successfully");

        }
    }
}
