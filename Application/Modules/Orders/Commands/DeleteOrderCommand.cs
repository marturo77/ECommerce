using Application.Modules.Orders.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Modules.Products.Commands
{
    /// <summary>
    /// Comando para eliminar una orden, la caracteristica de este comando es que la logica esta definido
    /// en terminos de abstracciones, de manera que asegura la portabilidad y la mantenibilidad
    /// </summary>
    public static class DeleteOrderCommand
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="OrderId"></param>
        public record RequestDeleteOrder(int OrderId) : IRequest<Response>
        {
        }

        /// <summary>
        /// Reglas de validacion en fluent validator, escritas explicitamente
        /// </summary>
        public class RequestValidator : AbstractValidator<RequestDeleteOrder>
        {
            /// <summary>
            ///
            /// </summary>
            public RequestValidator()
            {
                RuleFor(x => x.OrderId).GreaterThan(0);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public class Handler : IRequestHandler<RequestDeleteOrder, Response>
        {
            /// <summary>
            ///
            /// </summary>
            private readonly IValidator<RequestDeleteOrder> _validator;

            /// <summary>
            ///
            /// </summary>
            private readonly IOrderRepository _order;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="validator"></param>
            public Handler(IValidator<RequestDeleteOrder> validator, IOrderRepository product)
            {
                _validator = validator;
                _order = product;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public async Task<Response> Handle(RequestDeleteOrder request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);

                if (validationResult.IsValid)
                {
                    var response = await _order.Delete(request.OrderId);
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