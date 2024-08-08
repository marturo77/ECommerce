using Application.Modules.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
///  En general no se trabajo en el manejador de excepciones dado el alcance del tiempo
///  para desarrollar la prueba tecnica
/// </summary>
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
        group.MapPut("", UpdateProduct);
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
    private static async Task<IResult> UpdateProduct([FromBody] UpdateProductCommand.RequestUpdate request, ISender sender,
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
    [HttpDelete("{id}")]
    public static async Task<IResult> DeleteProduct(int? id, [FromServices] ISender sender, CancellationToken cancellationToken)
    {
        if (id.HasValue)
        {
            try
            {
                var request = new DeleteProductCommand.RequestDelete(id.Value);
                var response = await sender.Send(request);
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        }
        else
        {
            return Results.BadRequest(new { message = "No se ha incluido un Id" });
        }
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