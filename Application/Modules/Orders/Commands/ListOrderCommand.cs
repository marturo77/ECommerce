using Application.Modules.Orders.Models;
using Application.Modules.Orders.Queries;
using FluentValidation;
using MediatR;

namespace Application.Modules.Products.Commands
{
    /// <summary>
    /// Lista los productos, en si el comando constituye una abstraccion de la funcionalidad
    /// Se destaca porque sus propiedades son completamente todas interfaces
    /// </summary>
    public static class ListOrderCommand
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public record RequestListOrder(string? Name) : IRequest<Response>
        {
        }

        /// <summary>
        ///
        /// </summary>
        public class Handler : IRequestHandler<RequestListOrder, Response>
        {
            /// <summary>
            ///
            /// </summary>
            private readonly IOrderQuery _order;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="validator"></param>
            public Handler(IOrderQuery order)
            {
                _order = order;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public async Task<Response> Handle(RequestListOrder request, CancellationToken cancellationToken)
            {
                // Se usa notacion Async porque la mayoria de linters lo revisan y validan de esta forma
                var response = await _order.ListAsync(request.Name);
                return new Response(response);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        public record Response(ICollection<OrderInfo>? items);
    }
}