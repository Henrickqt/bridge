using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Infra.IoC
{
    public static class DIConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            InjectorBoostrapper.RegisterDbContext(services, configuration);
            InjectorBoostrapper.RegisterEventStreaming(services, configuration);
            InjectorBoostrapper.RegisterRepositories(services);
            InjectorBoostrapper.RegisterServices(services);
            InjectorBoostrapper.RegisterValidators(services);
            InjectorBoostrapper.RegisterHttpClients(services, configuration);
        }
    }
}
