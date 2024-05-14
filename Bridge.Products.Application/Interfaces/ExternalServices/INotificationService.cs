using Bridge.Products.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Interfaces.ExternalServices
{
    public interface INotificationService
    {
        Task SendNotification(GetOrderDto orderDto);
    }
}
