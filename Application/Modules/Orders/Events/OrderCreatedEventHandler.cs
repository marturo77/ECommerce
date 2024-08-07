using Application.Modules.Orders.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEventArgs>
{
    /// <summary>
    /// 
    /// </summary>
    private readonly IHubContext<NotificationHub> _hubContext;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hubContext"></param>
    public OrderCreatedEventHandler(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(OrderCreatedEventArgs notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"Se ha creado la orden {notification.orderId}");
    }
}