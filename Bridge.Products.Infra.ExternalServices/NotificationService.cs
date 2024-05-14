using Bridge.Products.Application.Exceptions;
using Bridge.Products.Application.Interfaces.ExternalServices;
using Bridge.Products.Application.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Infra.ExternalServices
{
    public class NotificationService : INotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendNotification(GetOrderDto orderDto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(orderDto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                //var response = await _httpClient.PostAsync("fact", data);
                var response = await _httpClient.GetAsync("fact");
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new BadRequestException($"Erro ao enviar notificação para o pedido {orderDto.OrderId}.");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
