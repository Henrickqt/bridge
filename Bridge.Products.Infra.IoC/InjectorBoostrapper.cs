using Bridge.Products.Application.Interfaces.Repositories;
using Bridge.Products.Infra.Data;
using Bridge.Products.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Infra.IoC
{
    public static class InjectorBoostrapper
    {
        public static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BridgeContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    opt => opt.MigrationsAssembly("Bridge.Products.Infra.Data"));
                options.UseLazyLoadingProxies();
            });
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
