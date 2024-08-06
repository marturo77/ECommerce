using Application.Modules.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Endpoints;

public static class OrderEndPoints
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    public static void RegisterOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/orders").WithTags("Orders");
        group.MapPost("", CreateOrder);
        group.MapGet("", ListOrder);
        group.MapDelete("{id:int}", DeleteOrder);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task<IResult> CreateOrder([FromBody] CreateOrderCommand.RequestCreateOrder request, ISender sender,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request);
        return Results.Ok(response);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task<IResult> DeleteOrder(int? id, ISender sender, CancellationToken cancellationToken)
    {
        if (id.HasValue)
        {
            DeleteOrderCommand.RequestDeleteOrder request = new DeleteOrderCommand.RequestDeleteOrder(id.Value);
            var response = await sender.Send(request);
            return Results.Ok(response);
        }
        else return Results.BadRequest("No se ha proporcionado identificador");
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task<IResult> ListOrder([FromQuery] string? name, ISender sender,
       CancellationToken cancellationToken)
    {
        ListOrderCommand.RequestListOrder request = new ListOrderCommand.RequestListOrder(name);
        var response = await sender.Send(request);
        return Results.Ok(response);
    }
}