using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TopStyle.Domain.Entities;
using TopStyle.Domain.Identity;
using TopStyleApi.Domain.Entities;

namespace TopStyle.Data
{
    public class TopStyleContext: IdentityDbContext<ApplicationUser>

    {
        public TopStyleContext(DbContextOptions<TopStyleContext> options):base (options)
        {
            
        }
        public  DbSet<Category> Categories { get; set; }
        public  DbSet<Order> Orders { get; set; }
        public  DbSet<OrderItem> OrderItems { get; set; }
        public  DbSet<Product> Products { get; set; }
    }
}

