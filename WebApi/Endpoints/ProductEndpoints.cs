using Application.Modules.Products.Commands;
using MediatR;

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
    private static async Task<IResult> CreateProduct(CreateProductCommand.RequestCreate request, ISender sender,
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
    private static async Task<IResult> DeleteProduct(DeleteProductCommand.RequestDelete request, ISender sender,
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
    private static async Task<IResult> ListProduct(ListProductCommand.RequestList request, ISender sender,
       CancellationToken cancellationToken)
    {
        var response = await sender.Send(request);
        return Results.Ok(response);
    }
}