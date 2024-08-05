using MediatR;

namespace Application.Modules.Products.Events
{
    /// <summary>
    ///  Evento cuando se crea un producto, podria devolver mas informacion
    ///  relevente al suceso en el patron de arquitectura de Responsabilidad
    ///  unica
    /// </summary>
    /// <param name="productId"></param>
    public record ProductCreatedEventArgs(int productId) : INotification;
}