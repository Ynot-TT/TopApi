using Microsoft.EntityFrameworkCore;
using TopStyle.Data;
using TopStyle.Domain.Entities;
using TopStyleApi.Data.Interfaces;
using TopStyleApi.Domain.Entities;

namespace TopStyleApi.Data.Repos
{
    public class OrderItemRepo : IOrderItemRepo
    {
        private readonly TopStyleContext _context;

        public OrderItemRepo(TopStyleContext context)
        {
            _context = context;
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
             _context.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            var orderItemToDelete = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItemToDelete != null )
            {
                _context.OrderItems.Remove(orderItemToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
        {
            return await _context.OrderItems.FindAsync(orderItemId);
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            var existingOrderItem = await _context.OrderItems.FindAsync(orderItem.OrderItemId);

            if (existingOrderItem != null )
            {
                existingOrderItem.ProductId = orderItem.ProductId;
                existingOrderItem.Quantity = orderItem.Quantity;
            }
            await _context.SaveChangesAsync();
        }
    }
}
