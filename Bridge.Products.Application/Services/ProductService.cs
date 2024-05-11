using Bridge.Products.Application.Exceptions;
using Bridge.Products.Application.Interfaces.Repositories;
using Bridge.Products.Application.Interfaces.Services;
using Bridge.Products.Application.Models;
using Bridge.Products.Domain.Entities;
using Bridge.Products.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProductDto> _createProductValidator;
        private readonly IValidator<UpdateProductDto> _updateProductValidator;

        public ProductService(IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            IValidator<CreateProductDto> createProductValidator,
            IValidator<UpdateProductDto> updateProductValidator)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _createProductValidator = createProductValidator;
            _updateProductValidator = updateProductValidator;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAsync();

            return products.Select(product => (GetProductDto)product);
        }

        public async Task<GetProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetAsync(productId);
            if (product == null)
                throw new NotFoundException("Produto não encontrado.");

            return (GetProductDto)product;
        }

        public async Task<GetProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            var errors = _createProductValidator.Validate(productDto).Errors;
            if (errors.Any())
                throw new BadRequestException("Produto apresenta campos inválidos.", errors);

            await VerifyProductNameExists(productDto.Name);

            var product = (Product)productDto;

            var result = await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();

            return (GetProductDto)result;
        }

        public async Task<GetProductDto> UpdateProductAsync(UpdateProductDto productDto, int productId)
        {
            var product = await _productRepository.GetAsync(productId);
            if (product == null)
                throw new NotFoundException("Produto não encontrado.");

            var errors = _updateProductValidator.Validate(productDto).Errors;
            if (errors.Any())
                throw new BadRequestException("Produto apresenta campos inválidos.", errors);

            await VerifyProductNameExists(productDto.Name, product.ProductId);

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Quantity = productDto.Quantity;

            var result = _productRepository.Update(product);
            await _unitOfWork.CommitAsync();

            return (GetProductDto)result;
        }

        public async Task<GetProductDto> DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetAsync(productId);
            if (product == null)
                throw new NotFoundException("Produto não encontrado.");

            var result = _productRepository.Remove(product);
            await _unitOfWork.CommitAsync();

            return (GetProductDto)result;
        }


        private async Task VerifyProductNameExists(string name, int? produtctId = null)
        {
            var product = produtctId == null
                ? await _productRepository.GetOneAsync(product => product.Name.ToLower() == name.ToLower().Trim())
                : await _productRepository.GetOneAsync(product => product.Name.ToLower() == name.ToLower().Trim() && product.ProductId != produtctId);

            if (product != null)
                throw new BadRequestException($"Já existe outro produto cadastrado com o nome {product.Name}.");
        }
    }
}
