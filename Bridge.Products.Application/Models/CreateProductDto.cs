using Bridge.Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Models
{
    public class CreateProductDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public static implicit operator CreateProductDto(Product product) => new()
        {
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
        };

        public static implicit operator Product(CreateProductDto productDto) => new()
        {
            Name = productDto.Name,
            Price = productDto.Price,
            Quantity = productDto.Quantity,
        };
    }
}
