using ECommerceShoppingApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceShoppingApplication.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext 
            (DbContextOptions<ApplicationDBContext> options) : base(options)
        { 
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Products> Products { get; set; }   
    }
}
