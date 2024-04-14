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
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(AddOrderDTO addOrderDTO)
        {

            // Log user's identity and claims
            var userIdentity = HttpContext.User.Identity.Name;
            var userClaims = HttpContext.User.Claims;
            _logger.LogInformation($"User {userIdentity} accessed AddOrder endpoint with claims: {string.Join(", ", userClaims)}");

            await _orderService.AddOrderAsync(addOrderDTO);
            return Ok("order created");
        }
    }
}
