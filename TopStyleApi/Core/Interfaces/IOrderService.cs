using TopStyle.Domain.Entities;
using TopStyleApi.Domain.DTO;

namespace TopStyleApi.Core.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(AddOrderDTO order, string userId);   
        Task<IEnumerable<GetOrderDTO>> GetOrdersByUserIdAsync(string userId);
    }
}
