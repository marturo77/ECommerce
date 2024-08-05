using Application.Modules.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Modules.Products.Commands.ListProductCommand;

namespace Todo.Api.Endpoints;

public static class ProductEndpoints
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    public static void RegisterProductsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/products").WithTags("Products");
        group.MapPost("", CreateProduct);
        group.MapGet("", ListProduct);
        group.MapDelete("", DeleteProduct);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task<IResult> CreateProduct([FromBody] CreateProductCommand.RequestCreate request, ISender sender,
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
    private static async Task<IResult> DeleteProduct([FromQuery] int id, ISender sender,
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
    private static async Task<IResult> ListProduct([FromQuery] string name, ISender sender,
       CancellationToken cancellationToken)
    {
        RequestList request = new RequestList(name);
        var response = await sender.Send(request);
        return Results.Ok(response);
    }
}