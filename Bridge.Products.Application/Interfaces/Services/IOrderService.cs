using Bridge.Products.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrderDto>> GetAllOrdersAsync();
        Task<GetOrderDto> GetOrderByIdAsync(int orderId);
        Task<GetOrderDto> CreateOrderAsync(CreateOrderDto orderDto);
        Task<GetOrderDto> CancelOrderAsync(int orderId);
        Task<GetOrderDto> ConfirmOrderAsync(int orderId);
    }
}
