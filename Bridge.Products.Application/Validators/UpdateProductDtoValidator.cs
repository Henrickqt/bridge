using Bridge.Products.Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Validators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .Length(1, 100).WithMessage("Nome deve possuir no mínimo 1 e no máximo 100 caracteres.");
            RuleFor(product => product.Price).GreaterThanOrEqualTo(0).WithMessage("Preço precisa ser maior ou igual a zero (0).");
            RuleFor(product => product.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantidade precisa ser maior ou igual a zero (0).");
        }
    }
}
