using Bridge.Products.Infra.Data;
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
    }
}
