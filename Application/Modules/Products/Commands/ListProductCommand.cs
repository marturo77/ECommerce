using Application.Modules.Products.Models;
using Application.Modules.Products.Queries;
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
        public record RequestList(string? Name) : IRequest<Response>
        {
        }

        /// <summary>
        ///
        /// </summary>
        public class Handler : IRequestHandler<RequestList, Response>
        {
            /// <summary>
            ///
            /// </summary>
            private readonly IProductQuery _product;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="validator"></param>
            public Handler(IProductQuery product)
            {
                _product = product;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public async Task<Response> Handle(RequestList request, CancellationToken cancellationToken)
            {
                // Se usa notacion Async porque la mayoria de linters lo revisan y validan de esta forma
                var response = await _product.ListAsync(request.Name);
                return new Response(response);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        public record Response(ICollection<ProductInfo>? items);
    }
}