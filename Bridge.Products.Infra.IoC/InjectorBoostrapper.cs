using Bridge.Products.Application.Interfaces.ExternalServices;
using Bridge.Products.Application.Interfaces.Repositories;
using Bridge.Products.Application.Interfaces.Services;
using Bridge.Products.Application.Models;
using Bridge.Products.Application.Services;
using Bridge.Products.Application.Validators;
using Bridge.Products.Domain.Interfaces;
using Bridge.Products.Infra.Data;
using Bridge.Products.Infra.Data.Kafka;
using Bridge.Products.Infra.Data.Repositories;
using Bridge.Products.Infra.ExternalServices;
using Confluent.Kafka;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

        public static void RegisterEventStreaming(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<KafkaConsumer>();
            services.AddSingleton<IKafkaProducer, KafkaProducer>(x => new KafkaProducer(new ProducerConfig
            {
                BootstrapServers = configuration["KafkaConfig:BootstrapServer"],
            }));
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
        }

        public static void RegisterValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateProductDto>, CreateProductDtoValidator>();
            services.AddScoped<IValidator<UpdateProductDto>, UpdateProductDtoValidator>();
            services.AddScoped<IValidator<CreateOrderDto>, CreateOrderDtoValidator>();
        }

        public static void RegisterHttpClients(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<INotificationService, NotificationService>(options =>
            {
                options.BaseAddress = new Uri(configuration["NotificationBaseUrl"]);
                options.DefaultRequestHeaders.Add("api_key", configuration["NotificationApiKey"]);
                options.Timeout = TimeSpan.FromSeconds(30);
            });
        }
    }
}
