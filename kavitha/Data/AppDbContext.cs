//using Microsoft.EntityFrameworkCore;
//using kavitha.Models;

//namespace kavitha.Data
//{
//  public class AppDbContext : DbContext
//  {
//    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//    public DbSet<User> Users { get; set; }
//    public DbSet<UserType> UserTypes { get; set; }


//    //protected override void OnModelCreating(ModelBuilder modelBuilder)
//    //{
//    //  modelBuilder.Entity<User>()
//    //      .HasOne(u => u.UserType)       // User has one UserType
//    //      .WithMany(ut => ut.Users)      // UserType has many Users
//    //      //.HasForeignKey(u => u.UserType)  // Foreign key mapping
//    //      .HasForeignKey(u => u.UserTypeId) // ✅ Correct: FK column should be UserTypeId
//    //      .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
//    //}

//    // ✅ Ensure Users table is defined
//    //public DbSet<User> Users { get; set; }
//    //public object UserTypes { get; internal set; }
//    //public DbSet<UserType> UserTypes { get; set; }
//    //public DbSet<UserType> UserTypes { get; set; } // This must be a DbSet

//    public DbSet<TravelRequest> TravelRequests { get; set; } // Add this line
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//      base.OnModelCreating(modelBuilder);
//    }
//  }
//}



using Microsoft.EntityFrameworkCore;
using kavitha.Models;

namespace kavitha.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<TravelRequest> TravelRequests { get; set; } // ✅ Ensure TravelRequests table exists

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder); // ✅ Call base first

      // ✅ Define Foreign Key Relationship for Users → UserTypes
      modelBuilder.Entity<User>()
          .HasOne(u => u.UserType)        // A User has one UserType
          .WithMany(ut => ut.Users)       // A UserType has many Users
          .HasForeignKey(u => u.UserTypeId) // Correct FK column should be UserTypeId
          .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
    }
  }
}
