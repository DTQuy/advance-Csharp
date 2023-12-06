using advance_Csharp.Database;
using advance_Csharp.Service.Interface;
using advance_Csharp.Service.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

namespace advance_Csharp
{
    public static class ServiceConfigurations
    {
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            _ = services.AddHttpContextAccessor();
            _ = services.AddScoped<IProductService, ProductService>();
            _ = services.AddScoped<IAuthenticationService, AuthenticationService>();
            _ = services.AddScoped<IApplicationService, ApplicationService>();
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<IRoleService, RoleService>();
            _ = services.AddScoped<IJwtService, JwtService>();



        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            _ = services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _ = services.AddDbContext<AdvanceCsharpContext>(opts =>
                    opts.UseSqlServer(connectionString));
        }
    }
}
