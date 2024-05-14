using Bridge.Products.Application.Models;
using Bridge.Products.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(order => order.Products).NotEmpty().WithMessage("Pedido precisa possuir no mínimo 1 produto.");
        }
    }
}
