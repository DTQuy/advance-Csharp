using advance_Csharp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Database
{
    public class AdvanceCsharpContext : DbContext
    {
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

        public DbSet<UserRole>? UserRoles { get; set; }

        public Task<int> SaveChangesAsync(string email)
        {
            throw new NotImplementedException();
        }

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
    }
}