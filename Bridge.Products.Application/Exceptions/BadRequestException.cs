using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Products.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<ValidationFailure> Errors { get; set; } = null!;

        public BadRequestException(string message) : base(message)
        {
            Errors = new List<ValidationFailure>();
        }

        public BadRequestException(string message, List<ValidationFailure> errors) : base(message)
        {
            Errors = errors ?? new List<ValidationFailure>();
        }
    }
}
