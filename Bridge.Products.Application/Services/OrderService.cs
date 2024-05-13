using Bridge.Products.Application.Exceptions;
using Bridge.Products.Application.Interfaces.Repositories;
using Bridge.Products.Application.Interfaces.Services;
using Bridge.Products.Application.Models;
using Bridge.Products.Domain.Entities;
using Bridge.Products.Domain.Enums;
using Bridge.Products.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<CreateOrderDto> _createOrderValidator;
        private readonly IProductService _productService;

        public OrderService(IUnitOfWork unitOfWork,
            IOrderRepository orderRepository,
            IValidator<CreateOrderDto> createOrderValidator,
            IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _createOrderValidator = createOrderValidator;
            _productService = productService;
        }

        public async Task<IEnumerable<GetOrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAsync();

            return orders.Select(order => (GetOrderDto)order);
        }

        public async Task<GetOrderDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);
            if (order == null)
                throw new NotFoundException("Pedido não encontrado.");

            return (GetOrderDto)order;
        }

        public async Task<GetOrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var errors = _createOrderValidator.Validate(orderDto).Errors;
                if (errors.Any())
                    throw new BadRequestException("Pedido apresenta campos inválidos.", errors);

                var products = await _productService.DecreaseProductsStockAsync(orderDto.Products.Distinct());

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    PaymentDate = null,
                    OrderStatus = EnOrderStatus.WaitingPayment,
                    Products = products.ToList(),
                };

                var result = await _orderRepository.AddAsync(order);

                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransactionAsync();

                return (GetOrderDto)result;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<GetOrderDto> CancelOrderAsync(int orderId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var order = await _orderRepository.GetAsync(orderId);
                if (order == null)
                    throw new NotFoundException("Pedido não encontrado.");

                if (order.OrderStatus == EnOrderStatus.Canceled)
                    throw new BadRequestException("O pedido já se encontra cancelado.");

                var productsName = order.Products.Select(Product => Product.Name);

                var products = await _productService.IncreaseProductsStockAsync(productsName);

                order.OrderStatus = EnOrderStatus.Canceled;

                var result = _orderRepository.Update(order);

                await _unitOfWork.CommitAsync();
                await _unitOfWork.CommitTransactionAsync();

                return (GetOrderDto)result;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<GetOrderDto> ConfirmOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);
            if (order == null)
                throw new NotFoundException("Pedido não encontrado.");

            if (order.OrderStatus != EnOrderStatus.WaitingPayment)
                throw new BadRequestException("Somente pedidos com status \"Aguardando Pagamento\" podem ser confirmados.");

            order.PaymentDate = DateTime.Now;
            order.OrderStatus = EnOrderStatus.Confirmed;

            var result = _orderRepository.Update(order);
            await _unitOfWork.CommitAsync();

            return (GetOrderDto)result;
        }
    }
}
