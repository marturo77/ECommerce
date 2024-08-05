using Application.Modules.Orders.Models;

namespace Application.Modules.Orders.Repositories
{
    /// <summary>
    ///
    /// </summary>
    internal interface IOrderRepository
    {
        /// <summary>
        /// Crea y actualiza un producto
        /// </summary>
        /// <param name="order">Orden de pedido</param>
        /// <returns></returns>
        Task<OrderInfo> Set(OrderInfo order);

        /// <summary>
        /// Borrar una orden de productos
        /// </summary>
        /// <param name="orderId">Identificador de la orden</param>
        /// <returns></returns>
        Task<bool> Delete(int orderId);
    }
}