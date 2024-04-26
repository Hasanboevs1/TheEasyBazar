using Microsoft.EntityFrameworkCore;
using TheEasyBazar.Domain.Entites;

namespace TheEasyBazar.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
        
    }
    public DbSet<User> Users { get; set; }
}
