using TopStyle.Domain.Entities;

namespace TopStyleApi.Domain.DTO
{
    public class GetOrderItemDTO
    {
        public string ProductName { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
    }
}
