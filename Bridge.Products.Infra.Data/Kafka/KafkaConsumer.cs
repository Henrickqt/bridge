using Bridge.Products.Application.Interfaces.ExternalServices;
using Bridge.Products.Application.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Infra.Data.Kafka
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KafkaConsumer(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(10000, stoppingToken);

            var config = new ConsumerConfig
            {
                GroupId = _configuration["KafkaConfig:ConsumerGroupId"],
                BootstrapServers = _configuration["KafkaConfig:BootstrapServer"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("send-notification");

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(stoppingToken);

                        var orderDto = JsonConvert.DeserializeObject<GetOrderDto>(consumeResult.Message.Value);
                        if (orderDto == null) continue;

                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                            await notificationService.SendNotification(orderDto);
                        }
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error occured: {e.Message}");
                    }
                }
            }
        }
    }
}
