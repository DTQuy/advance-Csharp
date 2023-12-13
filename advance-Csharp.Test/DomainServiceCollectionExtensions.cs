using advance_Csharp.Database;
using advance_Csharp.Service.Interface;
using advance_Csharp.Service.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace advance_Csharp.Test
{
    public static class DomainServiceCollectionExtensions
    {
        /// <summary>
        /// SetupProductService
        /// </summary>
        /// <returns></returns>
        public static IProductService SetupProductService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AdAddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IProductService? productService = serviceProvider.GetService<IProductService>() ??
                                throw new InvalidOperationException("IProductService not registered.");
            return productService;
        }

        /// <summary>
        /// SetupUserService
        /// </summary>
        /// <returns></returns>
        public static IUserService SetupUserService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AdAddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IUserService? userService = serviceProvider.GetService<IUserService>() ??
                            throw new InvalidOperationException("IUserService not registered.");

            return userService;
        }

        /// <summary>
        /// SetupCartService
        /// </summary>
        /// <returns></returns>
        public static ICartService SetupCartService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AdAddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            ICartService? cartService = serviceProvider.GetService<ICartService>() ??
                            throw new InvalidOperationException("ICartService not registered.");

            return cartService;
        }

        /// <summary>
        /// SetupOrderService
        /// </summary>
        /// <returns></returns>
        public static IOrderService SetupOrderService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AdAddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IOrderService? orderService = serviceProvider.GetService<IOrderService>() ??
                                throw new InvalidOperationException("IOrderService not registered.");

            return orderService;
        }

        /// <summary>
        /// AdAddDomainServices
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection AdAddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            string connectionString = "Data Source=sql.bsite.net\\MSSQL2016; Initial Catalog=dangthanhquy2303_AdvanceCsharp;User ID=dangthanhquy2303_AdvanceCsharp;Password=Thanhquy12345@; TrustServerCertificate=False";

            _ = services.AddDbContext<AdvanceCsharpContext>(opts =>
                    opts.UseSqlServer(connectionString));


            _ = services.AddScoped<IProductService, ProductService>();
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<ICartService, CartService>();
            _ = services.AddScoped<IOrderService, OrderService>();
            _ = services.AddHttpContextAccessor();
            return services;
        }
    }
}
