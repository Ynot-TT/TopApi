﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopStyle.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public virtual User user { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
