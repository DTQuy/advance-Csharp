using advance_Csharp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Database
{
    public class AdvanceCsharpContext : DbContext
    {

        /// <summary>
        /// Connectionstring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public AdvanceCsharpContext(DbContextOptions<AdvanceCsharpContext> options) : base(options)
        {
        }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<UserRole>()
                .HasKey(e => new { e.UserId, e.RoleId });
        }

        /// <summary>
        /// AppVersions
        /// </summary>
        public DbSet<AppVersion>? AppVersions { get; set; }

        /// <summary>
        /// Products
        /// </summary>
        public DbSet<Product>? Products { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User>? Users { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        public DbSet<Role>? Roles { get; set; }

        /// <summary>
        /// UserRoles
        /// </summary>
        public DbSet<UserRole>? UserRoles { get; set; }

        /// <summary>
        /// Cart
        /// </summary>
        public DbSet<Cart> Carts { get; set; } = null!;

        /// <summary>
        /// Cart detail
        /// </summary>
        public DbSet<CartDetail>? CartDetails { get; set; }

        /// <summary>
        /// Orders
        /// </summary>
        public DbSet<Order>? Orders { get; set; }

        /// <summary>
        /// OrderDetails
        /// </summary>
        public DbSet<OrderDetail>? OrderDetails { get; set; }

        public Task<int> SaveChangesAsync(string email)
        {
            throw new NotImplementedException();
        }


    }
}