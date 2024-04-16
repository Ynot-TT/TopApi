using Microsoft.EntityFrameworkCore;
using TopStyle.Data;
using TopStyle.Domain.Entities;
using TopStyleApi.Data.Interfaces;

namespace TopStyleApi.Data.Repos
{
    public class OrderRepo : IOrderRepo
    {
        private readonly TopStyleContext _context;

        public OrderRepo(TopStyleContext context)
        {
            _context = context;
        }



        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            _context.SaveChanges();
        }

        public async Task DeleteOrderAsync(int orderId, string userId)
        {
            //var orderToDelete = await _context.Orders.FindAsync(orderId);
            //if (orderToDelete != null && orderToDelete.UserId == userId)
            //{
            //    _context.Orders.Remove(orderToDelete);
            //    await _context.SaveChangesAsync();
            //}

        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var products = await _context.Orders.ToListAsync();
            return products;
        }

        public async Task<Order> GetOrderIByIdAsync(int orderId)
        {
            var product = await _context.Orders.FindAsync(orderId);
                return product;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)  
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return orders;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            return;
        }
    }
}
