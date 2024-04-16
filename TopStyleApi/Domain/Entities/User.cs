﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TopStyle.Domain.Entities
{
    public class User:IdentityUser
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
