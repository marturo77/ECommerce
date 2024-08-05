using Application.Modules.Products.Models;
using Application.Modules.Products.Queries;
using FluentValidation;
using MediatR;

namespace Application.Modules.Products.Commands
{
    /// <summary>
    /// Lista los productos, en si el comando constituye una abstraccion de la funcionalidad
    /// Se destaca porque sus propiedades son completamente todas interfaces
    /// </summary>
    public static class ListProductCommand
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public record Request(string Name) : IRequest<Response>
        {
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
            private readonly IProductQuery _product;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="validator"></param>
            public Handler(IValidator<Request> validator, IProductQuery product)
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
                    // Se usa notacion Async porque la mayoria de linters lo revisan y validan de esta forma
                    var response = await _product.ListAsync(request.Name);
                    return new Response(response);
                }
                else return new Response(null);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        public record Response(ICollection<ProductInfo>? items);
    }
}