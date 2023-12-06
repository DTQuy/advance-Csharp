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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=dangthanhquy2303_AdvanceCsharp;User Id=dangthanhquy2303_AdvanceCsharp;Password=Thanhquy12345@;Trusted_Connection=False;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<UserRole>()
                .HasKey(e => new { e.UserId, e.RoleId });

        }
    }
}