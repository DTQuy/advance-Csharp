using advance_Csharp.Database;
using advance_Csharp.Service.Interface;
using advance_Csharp.Service.Service;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp
{
    public static class ServiceConfigurations
    {
        /// <summary>
        /// ConfigureServiceManager
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            _ = services.AddHttpContextAccessor();
            _ = services.AddScoped<IProductService, ProductService>();
            _ = services.AddScoped<IAuthenticationService, AuthenticationService>();
            _ = services.AddScoped<IApplicationService, ApplicationService>();
            /* _ = services.AddTransient<IUserService, UserService>();*/
            _ = services.AddScoped<IUserService>(provider =>
           {
               return new UserService(
                   provider.GetRequiredService<IHttpContextAccessor>(),
                   provider.GetRequiredService<DbContextOptions<AdvanceCsharpContext>>()
               );
           });

            _ = services.AddScoped<IRoleService, RoleService>();
            _ = services.AddScoped<IJwtService, JwtService>();
            _ = services.AddScoped<IUserRoleService, UserRoleService>();
            _ = services.AddScoped<IUnitWork, UnitWork>();
            _ = services.AddScoped<ICartService, CartService>();
            _ = services.AddScoped<IOrderService, OrderService>();



        }

        /// <summary>
        /// ConfigureCors
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            _ = services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });
        }

        /// <summary>
        /// ConfigureSqlContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _ = services.AddDbContext<AdvanceCsharpContext>(opts =>
           opts.UseSqlServer(connectionString));
        }


    }
}
