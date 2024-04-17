using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TopStyle.Data;
using TopStyle.Domain.Entities;
using TopStyleApi.Core.Interfaces;
using TopStyleApi.Data.Interfaces;
using TopStyleApi.Domain.DTO;
using TopStyleApi.Domain.Entities;

namespace TopStyleApi.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepo _orderRepo;
        private readonly IProductService _productService;
        private readonly TopStyleContext _topStyleContext;

        public OrderService(IMapper mapper, IOrderRepo orderRepo, IProductService productService, TopStyleContext topStyleContext)
        {
            _mapper = mapper;
            _orderRepo = orderRepo;
            _productService = productService;
            _topStyleContext = topStyleContext;
        }

        public async Task AddOrderAsync(AddOrderDTO addOrderDTO, string userId)
        {
            Order order = new Order
            {
                UserId = userId,
                Items = addOrderDTO.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                }).ToList()
            };

            int? totalPrice = await CalculateTotalOrderPrice(addOrderDTO);

            if (totalPrice.HasValue)
            {
                order.TotalPrice = totalPrice.Value;

                await _orderRepo.AddOrderAsync(order);
            }
        }

        public async Task<int?> CalculateTotalOrderPrice(AddOrderDTO orderDTO)
        {
            int totalPrice = 0;

            foreach (var item in orderDTO.Items)
            {
                int? productPrice = await _productService.GetProductPrice(item.ProductId);
                if (productPrice.HasValue)
                {
                    totalPrice += productPrice.Value * item.Quantity;
                }
                else
                {
                }
            }

            return totalPrice;
        }
        public async Task<IEnumerable<GetOrderDTO>> GetOrdersByUserIdAsync(string userId)
        {

            var orders = await _orderRepo.GetOrdersByUserIdAsync(userId);
            var orderDTOs = new List<GetOrderDTO>();

            foreach (var order in orders)
            {
                var orderDTO = new GetOrderDTO
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice,
                    Items = new List<GetOrderItemDTO>()
                };

                foreach (var item in order.Items)
                {
                    var product = await _topStyleContext.Products.FindAsync(item.ProductId);
                    var itemDTO = new GetOrderItemDTO
                    {
                        ProductName = product.ProductName, 
                        Price = product.Price + " kr" ,
                        Quantity = item.Quantity
                    };
                    orderDTO.Items.Add(itemDTO);
                }

                orderDTOs.Add(orderDTO);
            }

            return orderDTOs;
        }
    }
}
