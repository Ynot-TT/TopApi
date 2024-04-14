﻿using System.ComponentModel.DataAnnotations;
using TopStyle.Domain.Entities;

namespace TopStyleApi.Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        //Price där Quantity * Product.Price, var product = GetProductById(productID), product.price
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }    
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }



    }
}
