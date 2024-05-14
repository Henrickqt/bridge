using Bridge.Products.Application.Interfaces.Services;
using Bridge.Products.Application.Models;
using Bridge.Products.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bridge.Products.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetOrderDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(GetOrderDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int orderId)
        {
            var result = await _orderService.GetOrderByIdAsync(orderId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetOrderDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto orderDto)
        {
            var result = await _orderService.CreateOrderAsync(orderDto);
            var uri = $"{HttpContext.Request.Path}/{result.OrderId}";
            return Created(uri, result);
        }

        [HttpPut("{orderId}/cancel")]
        [ProducesResponseType(typeof(GetOrderDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Cancel([FromRoute] int orderId)
        {
            var result = await _orderService.CancelOrderAsync(orderId);
            return Ok(result);
        }

        [HttpPut("{orderId}/confirm")]
        [ProducesResponseType(typeof(GetOrderDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Confirm([FromRoute] int orderId)
        {
            var result = await _orderService.ConfirmOrderAsync(orderId);
            return Ok(result);
        }
    }
}
