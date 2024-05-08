using Bridge.Products.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public EnOrderStatus OrderStatus { get; set; }
        public virtual ICollection<Product> Products { get; set; } = null!;
    }
}
