using Application.Modules.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        group.MapDelete("{id:int?}", DeleteProduct);
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
    private static async Task<IResult> DeleteProduct(int? id, ISender sender,
       CancellationToken cancellationToken)
    {
        if (id.HasValue)
        {
            try
            {
                DeleteProductCommand.RequestDelete request = new DeleteProductCommand.RequestDelete(id.Value);
                var response = await sender.Send(request);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
        else return Results.BadRequest("No se ha incluido un Id");
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="sender"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task<IResult> ListProduct([FromQuery] string? name, ISender sender,
       CancellationToken cancellationToken)
    {
        ListProductCommand.RequestList request = new ListProductCommand.RequestList(name);
        var response = await sender.Send(request);
        return Results.Ok(response);
    }
}