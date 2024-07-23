using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<IdentityUser, IdentityRole, string>(options)
{
   public DbSet<Product> Products { get; set; }
}
