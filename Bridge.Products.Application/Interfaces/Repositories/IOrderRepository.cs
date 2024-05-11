using Bridge.Products.Domain.Entities;
using Bridge.Products.Domain.Enums;
using Bridge.Products.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(EnOrderStatus orderStatus, bool tracking);
    }
}
