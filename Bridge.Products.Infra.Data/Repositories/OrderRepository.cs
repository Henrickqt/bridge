using Bridge.Products.Application.Interfaces.Repositories;
using Bridge.Products.Domain.Entities;
using Bridge.Products.Domain.Enums;
using Bridge.Products.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Infra.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BridgeContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(EnOrderStatus orderStatus, bool tracking)
        {
            return await GetAsync(o => o.OrderStatus == orderStatus, tracking);
        }
    }
}
