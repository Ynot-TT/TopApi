using TopStyle.Domain.Entities;
using TopStyleApi.Domain.Entities;

namespace TopStyleApi.Data.Interfaces
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderIByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId, string userId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);


    }
}
