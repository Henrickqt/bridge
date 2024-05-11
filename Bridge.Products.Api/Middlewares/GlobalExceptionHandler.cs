using Bridge.Products.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Bridge.Products.Api.Middlewares
{
    public static class GlobalExceptionHandler
    {
        public static async Task Handle(HttpContext httpContext)
        {
            var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();

            if (exceptionHandlerFeature is null)
                return;

            var (httpStatusCode, message) = exceptionHandlerFeature.Error switch
            {
                NotFoundException ex => (HttpStatusCode.NotFound, ex.Message),
                BadRequestException ex => (HttpStatusCode.BadRequest, ex.Message),
                _ => (HttpStatusCode.InternalServerError, "Erro inesperado.")
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)httpStatusCode;

            var errors = exceptionHandlerFeature.Error is BadRequestException
                ? (exceptionHandlerFeature.Error as BadRequestException ?? new BadRequestException(""))
                    .Errors.Select(error => $"{error.PropertyName}: {error.ErrorMessage}")
                : new List<string>();

            var jsonResponse = new
            {
                httpContext.Response.StatusCode,
                Message = message,
                Errors = errors,
            };

            var jsonSerialized = JsonSerializer.Serialize(jsonResponse);
            await httpContext.Response.WriteAsync(jsonSerialized);
        }
    }
}
