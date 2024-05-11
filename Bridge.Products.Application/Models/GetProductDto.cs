using Bridge.Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Models
{
    public class GetProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public static implicit operator GetProductDto(Product product) => new()
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
        };

        public static implicit operator Product(GetProductDto productDto) => new()
        {
            ProductId = productDto.ProductId,
            Name = productDto.Name,
            Price = productDto.Price,
            Quantity = productDto.Quantity,
        };
    }
}
