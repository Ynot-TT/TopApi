using AutoMapper;
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

        public OrderService(IMapper mapper, IOrderRepo orderRepo, IProductService productService)
        {
            _mapper = mapper;
            _orderRepo = orderRepo;
            _productService = productService;
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

        public async Task<int> CalculateTotalOrderPrice(AddOrderDTO orderDTO)
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
        public Task DeleteOrderAsync(int orderId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderIByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderAsync(AddOrderDTO order)
        {
            throw new NotImplementedException();
        }

        
    }
}
