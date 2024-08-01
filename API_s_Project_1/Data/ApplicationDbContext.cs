using API_s_Project_1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_s_Project_1.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Category> categories { get; set; }

        public DbSet<Product> products { get; set; }

        public DbSet<ShoppingCart> shoppingCarts { get; set; }

        public DbSet<OrderHeader> orderHeaders { get; set; }

        public DbSet<OrderDetail> orderDetails { get; set; }
    }
}
