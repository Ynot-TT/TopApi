using TopStyle.Domain.Entities;
using TopStyleApi.Domain.Entities;

namespace TopStyleApi.Data.Interfaces
{
    public interface IOrderItemRepo
    {
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem> GetOrderItemByIdAsync(int orderItemId);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(int orderItemId);
    }
}
