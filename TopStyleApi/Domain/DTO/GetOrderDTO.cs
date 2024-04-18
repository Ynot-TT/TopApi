using System.ComponentModel.DataAnnotations.Schema;
using TopStyle.Domain.Identity;
using TopStyleApi.Domain.Entities;

namespace TopStyleApi.Domain.DTO
{
    public class GetOrderDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<GetOrderItemDTO> Items { get; set; }
        public string TotalPrice { get; set; }
    }
}
