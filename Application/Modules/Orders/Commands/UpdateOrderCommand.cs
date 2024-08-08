using Application.Modules.Orders.Events;
using Application.Modules.Orders.Models;
using Application.Modules.Orders.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Modules.Products.Commands
{
    /// <summary>
    /// Este comando se coloca el el codigo pero no se usa para la prueba debido al alcance para desarrollar esta prueba tecnica
    /// </summary>
    public static class UpdateOrderCommand
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        public record RequestUpdateOrder(int OrderId, DateTime OrderDate, string CustomerName, string Status, ICollection<OrderItemInfo> OrderItems, decimal Total) : IRequest<Response>
        {
            /// <summary>
            ///  Conversion usando el compilador de entidades, convierte un request a un objeto de negocio
            /// </summary>
            /// <param name="request"></param>
            public static implicit operator OrderInfo(RequestUpdateOrder request) =>
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
        public class RequestValidator : AbstractValidator<RequestUpdateOrder>
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
        public class Handler : IRequestHandler<RequestUpdateOrder, Response>
        {
            /// <summary>
            ///
            /// </summary>
            private readonly IValidator<RequestUpdateOrder> _validator;

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
            public Handler(IValidator<RequestUpdateOrder> validator, IOrderRepository order, IPublisher publisher)
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
            public async Task<Response> Handle(RequestUpdateOrder request, CancellationToken cancellationToken)
            {
                var validationResult = _validator.Validate(request);

                if (validationResult.IsValid)
                {
                    var response = await _order.Set(request);

                    if (response is not null)
                    {
                        // Notifica que la orden fue creada correctamente usando emisor de eventos de MediatR
                        // Para el update seguramente se necesita otro evento diferente para ser notificado en la aplicacion angular
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