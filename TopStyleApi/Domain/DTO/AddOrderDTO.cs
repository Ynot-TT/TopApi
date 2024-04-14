using TopStyle.Domain.Entities;
using TopStyleApi.Domain.Entities;

namespace TopStyleApi.Domain.DTO
{
    public class AddOrderDTO
    {
        public int UserId { get; set; }
        public List<AddOrderItemDTO> Items { get; set; }
    }
}
