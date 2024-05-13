using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Models
{
    public class CreateOrderDto
    {
        public IEnumerable<string> Products { get; set; } = null!;
    }
}
