using Bridge.Products.Application.Interfaces.Services;
using Bridge.Products.Application.Models;
using Bridge.Products.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bridge.Products.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetProductDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(IEnumerable<GetProductDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<GetProductDto>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int productId)
        {
            var result = await _productService.GetProductByIdAsync(productId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<GetProductDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(IEnumerable<GetProductDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto productDto)
        {
            var result = await _productService.CreateProductAsync(productDto);
            var uri = $"{HttpContext.Request.Path}/{result.ProductId}";
            return Created(uri, result);
        }

        [HttpPatch("{productId}")]
        [ProducesResponseType(typeof(GetProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto productDto, [FromRoute] int productId)
        {
            var result = await _productService.UpdateProductAsync(productDto, productId);
            return Ok(result);
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(typeof(GetProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            var result = await _productService.DeleteProductAsync(productId);
            return Ok(result);
        }
    }
}
