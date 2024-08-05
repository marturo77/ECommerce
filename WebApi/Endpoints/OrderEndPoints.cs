using Application.Modules.Products.Commands;
using MediatR;

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
        group.MapDelete("", DeleteOrder);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task<IResult> CreateOrder(CreateProductCommand.RequestCreate request, ISender sender,
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
    private static async Task<IResult> DeleteOrder(DeleteProductCommand.RequestDelete request, ISender sender,
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
    private static async Task<IResult> ListOrder(ListProductCommand.RequestList request, ISender sender,
       CancellationToken cancellationToken)
    {
        var response = await sender.Send(request);
        return Results.Ok(response);
    }
}