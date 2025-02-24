using Microsoft.EntityFrameworkCore;
using kavitha.Models;

namespace kavitha.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ✅ Ensure Users table is defined
    public DbSet<User> Users { get; set; }
  }
}
