using TopStyle.Domain.Entities;
using TopStyleApi.Domain.Entities;

namespace TopStyleApi.Domain.DTO
{
    public class GetOrderBySearchDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public int TotalPrice { get; set; }
    }
}
