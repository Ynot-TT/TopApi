using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
          public async Task<ActionResult> AddOrder(AddOrderDTO addOrderDTO)
          {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User ID is missing from the token.");
                }
                try
                {
                    await _orderService.AddOrderAsync(addOrderDTO, userId);
                    return Ok("Order created successfully.");
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest($"Invalid data: {ex.Message}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Request failed: {ex.Message}");
                }
          }
        [HttpGet("own")]
        public async Task<ActionResult> GetOwnOrder()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing from the token.");
            }

            try
            {
                var order = await _orderService.GetOrdersByUserIdAsync(userId);
                if (order == null)
                {
                    return NotFound("Order not found for the current user.");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Request failed: {ex.Message}");
            }
        }
    }
}
