using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopStyle.Domain.Identity;
using TopStyleApi.Domain.Entities;

namespace TopStyle.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public int TotalPrice { get; set; }
        public Order()
        {
            Items = new List<OrderItem>();
        }
    }
}
