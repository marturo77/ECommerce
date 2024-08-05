using Application.Modules.Products.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Modules.Products.Commands
{
    /// <summary>
    /// Comando para Crear un producto, la caracteristica de este comando es que la logica esta definido
    /// en terminos de abstracciones, de manera que asegura la portabilidad y la mantenibilidad
    /// </summary>
    public static class DeleteProductCommand
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="ProductId"></param>
        public record Request(int ProductId) : IRequest<Response>
        {
        }

        /// <summary>
        /// Reglas de validacion en fluent validator, escritas explicitamente
        /// </summary>
        public class RequestValidator : AbstractValidator<Request>
        {
            /// <summary>
            ///
            /// </summary>
            public RequestValidator()
            {
                RuleFor(x => x.ProductId).GreaterThan(0);
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
            private readonly IProductRepository _product;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="validator"></param>
            public Handler(IValidator<Request> validator, IProductRepository product)
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
                    var response = await _product.Delete(request.ProductId);
                    return new Response(response);
                }
                else return new Response(false);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wasDeleted"></param>
        public record Response(bool wasDeleted);
    }
}