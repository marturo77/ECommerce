using Application.Modules.Products.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEventArgs>
{
    /// <summary>
    ///
    /// </summary>
    private readonly IHubContext<NotificationHub> _hubContext;

    /// <summary>
    ///
    /// </summary>
    /// <param name="hubContext"></param>
    public ProductCreatedEventHandler(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="notification"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(ProductCreatedEventArgs notification, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"Se ha creado el producto {notification.productId}");
    }
}