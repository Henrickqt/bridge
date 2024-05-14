using Bridge.Products.Domain.Entities;
using Bridge.Products.Domain.Enums;
using Bridge.Products.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Models
{
    public class GetOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string OrderStatus { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public IEnumerable<GetProductDto> Products { get; set; } = null!;

        public static implicit operator GetOrderDto(Order order) => new()
        {
            OrderId = order.OrderId,
            OrderDate = order.OrderDate,
            PaymentDate = order.PaymentDate,
            OrderStatus = order.OrderStatus.GetDescription(),
            TotalPrice = order.Products.Sum(product => product.Price),
            Products = order.Products.Select(product => (GetProductDto)product).ToList(),
        };
    }
}
