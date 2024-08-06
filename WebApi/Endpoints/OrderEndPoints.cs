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
    private static async Task<IResult> CreateOrder([FromBody] CreateProductCommand.RequestCreate request, ISender sender,
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
    private static async Task<IResult> DeleteOrder([FromQuery] int id, ISender sender,
       CancellationToken cancellationToken)
    {
        DeleteProductCommand.RequestDelete request = new DeleteProductCommand.RequestDelete(id);
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
    private static async Task<IResult> ListOrder([FromQuery] string name, ISender sender,
       CancellationToken cancellationToken)
    {
        ListProductCommand.RequestList request = new ListProductCommand.RequestList(name);
        var response = await sender.Send(request);
        return Results.Ok(response);
    }
}