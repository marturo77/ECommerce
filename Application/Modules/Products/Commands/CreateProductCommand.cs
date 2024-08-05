using Application.Modules.Products.Models;
using Application.Modules.Products.Repositories;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Modules.Products.Commands
{
    /// <summary>
    /// Comando para Crear un producto, la caracteristica de este comando es que la logica esta definido
    /// en terminos de abstracciones, de manera que asegura la portabilidad y la mantenibilidad
    /// </summary>
    public static class CreateProductCommand
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        public record Request(int ProductId, string Name, string Category, string Description, int Stock, decimal Price) : IRequest<Response>
        {
            /// <summary>
            ///  Conversion usando el compilador de entidades, convierte un request a un objeto de negocio
            /// </summary>
            /// <param name="request"></param>
            public static implicit operator ProductInfo(Request request) =>
                new()
                {
                    Name = request.Name,
                    Category = request.Category,
                    Description = request.Description,
                    Price = request.Price,
                    ProductId = request.ProductId,
                    Stock = request.Stock
                };
        }

        /// <summary>
        /// Reglas de validacion en fluent validator, escritas explicitamente
        /// </summary>
        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x).NotNull();
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(256);

                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Price).NotNull();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public class Handler : IRequestHandler<Request, Response>
        {
            /// <summary>
            ///
            /// </summary>
            private readonly IValidator<Request> _validator;

            /// <summary>
            ///
            /// </summary>
            private readonly IProduct _product;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="validator"></param>
            public Handler(IValidator<Request> validator, IProduct product)
            {
                _validator = validator;
                _product = product;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);

                if (validationResult.IsValid)
                {
                    var response = await _product.Set(request);
                    return new Response(response.ProductId);
                }
                else return new Response(0);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        public record Response(int productId);
    }
}