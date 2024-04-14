using AutoMapper;
using TopStyle.Domain.Entities;
using TopStyleApi.Core.Interfaces;
using TopStyleApi.Data.Interfaces;
using TopStyleApi.Domain.DTO;

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

        public async Task AddOrderAsync(AddOrderDTO addOrderDTO)
        {
            var order = new Order
            {
                UserId = addOrderDTO.UserId
            };

            // Calculate total price
            int? totalPrice = await CalculateTotalOrderPrice(addOrderDTO);

            if (totalPrice.HasValue)
            {
                // Set total price
                order.TotalPrice = totalPrice.Value;

                // Add order to repository
                await _orderRepo.AddOrderAsync(order);
            }
            else
            {
                // Handle scenario where total price calculation failed
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
                    // Handle scenario where product price is not available
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
