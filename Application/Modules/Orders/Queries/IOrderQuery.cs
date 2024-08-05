using Application.Modules.Products.Models;

namespace Application.Modules.Orders.Queries
{
    /// <summary>
    /// Abstraccion para la ejecucion de consultas relacionadas con ordenes de productos
    /// </summary>
    public interface IOrderQuery
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        Task<ICollection<OrderInfo>> ListAsync(string customerName);
    }
}