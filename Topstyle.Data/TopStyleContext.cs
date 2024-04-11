using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;

namespace TopStyle.Data
{
    public class TopStyleContext: IdentityDbContext<ApplicationUser>

    {
        public TopStyleContext(DbContextOptions<TopStyleContext> options):base (options)
        {
            
        }
        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

    }
}

