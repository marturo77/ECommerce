using MediatR;

namespace Application.Modules.Orders.Events
{
    /// <summary>
    ///
    /// </summary>
    public record OrderCreatedEventArgs(int orderId) : INotification;
}