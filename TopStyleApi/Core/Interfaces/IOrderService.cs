using TopStyle.Domain.Entities;
using TopStyleApi.Domain.DTO;

namespace TopStyleApi.Core.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderIByIdAsync(int orderId);
        Task AddOrderAsync(AddOrderDTO order, string userId);
        Task UpdateOrderAsync(AddOrderDTO order);
        Task DeleteOrderAsync(int orderId, int userId);
        Task<IEnumerable<GetOrderDTO>> GetOrdersByUserIdAsync(string userId);
    }
}
