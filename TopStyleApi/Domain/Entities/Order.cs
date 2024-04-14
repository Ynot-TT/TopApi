using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopStyleApi.Domain.Entities;

namespace TopStyle.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public int TotalPrice { get; set; }
    }
}
