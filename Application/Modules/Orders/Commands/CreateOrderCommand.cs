using Application.Modules.Orders.Events;
using Application.Modules.Orders.Models;
using Application.Modules.Orders.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Modules.Products.Commands
{
    /// <summary>
    /// Comando para Crear una orden de productos, la caracteristica de este comando es que la logica esta definido
    /// en terminos de abstracciones, de manera que asegura la portabilidad y la mantenibilidad
    /// </summary>
    public static class CreateOrderCommand
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        public record RequestCreateOrder(int OrderId, DateTime OrderDate, string CustomerName, string Status, ICollection<OrderItemInfo> OrderItems, decimal Total) : IRequest<Response>
        {
            /// <summary>
            ///  Conversion usando el compilador de entidades, convierte un request a un objeto de negocio
            /// </summary>
            /// <param name="request"></param>
            public static implicit operator OrderInfo(RequestCreateOrder request) =>
                new()
                {
                    OrderDate = request.OrderDate,
                    CustomerName = request.CustomerName,
                    OrderItems = request.OrderItems,
                    Status = request.Status,
                    Total = request.Total,
                    OrderId = request.OrderId
                };
        }

        /// <summary>
        /// Reglas de validacion en fluent validator, escritas explicitamente
        /// </summary>
        public class RequestValidator : AbstractValidator<RequestCreateOrder>
        {
            public RequestValidator()
            {
                RuleFor(x => x).NotNull();
                RuleFor(x => x.CustomerName)
                    .NotEmpty()
                    .MaximumLength(256);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public class Handler : IRequestHandler<RequestCreateOrder, Response>
        {
            /// <summary>
            ///
            /// </summary>
            private readonly IValidator<RequestCreateOrder> _validator;

            /// <summary>
            ///
            /// </summary>
            private readonly IPublisher _publisher;

            /// <summary>
            ///
            /// </summary>
            private readonly IOrderRepository _order;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="validator"></param>
            public Handler(IValidator<RequestCreateOrder> validator, IOrderRepository order, IPublisher publisher)
            {
                _validator = validator;
                _order = order;
                _publisher = publisher;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            /// <exception cref="NotImplementedException"></exception>
            public async Task<Response> Handle(RequestCreateOrder request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);

                if (validationResult.IsValid)
                {
                    var response = await _order.Set(request);

                    if (response is not null)
                    {
                        // Notifica que la orden fue creada correctamente usando emisor de eventos de MediatR
                        await _publisher.Publish(new OrderCreatedEventArgs(response.OrderId));
                        return new Response(response);
                    }
                    else return new Response(null);
                }
                else return new Response(null);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        public record Response(OrderInfo? order);
    }
}