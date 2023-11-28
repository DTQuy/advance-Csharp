using advance_Csharp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp.Database
{
    public class AdvanceCsharpContext : DbContext
    {
        public DbSet<AppVersion>? AppVersions { get; set; }
        public DbSet<Product>? Products { get; set; }

        public DbSet<User>? Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=dangthanhquy2303_AdvanceCsharp;User Id=dangthanhquy2303_AdvanceCsharp;Password=Thanhquy12345@;Trusted_Connection=False;");
        }
    }
}