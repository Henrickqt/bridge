using Bridge.Products.Application.Models;
using Bridge.Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDto>> GetAllProductsAsync();
        Task<GetProductDto> GetProductByIdAsync(int productId);
        Task<GetProductDto> CreateProductAsync(CreateProductDto productDto);
        Task<GetProductDto> UpdateProductAsync(UpdateProductDto productDto, int productId);
        Task<GetProductDto> DeleteProductAsync(int productId);
        Task<IEnumerable<Product>> DecreaseProductsStockAsync(IEnumerable<string> names);
        Task<IEnumerable<Product>> IncreaseProductsStockAsync(IEnumerable<string> names);
    }
}
