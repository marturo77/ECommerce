using Application.Modules.Products.Models;

namespace Application.Modules.Products.Queries
{
    /// <summary>
    /// Abstraccion para operaciones de lectura, esta aquitectura permite pensar en posibilidades
    /// como bases de datos de solo lectura on en modo replica
    ///
    /// Recordar que las interfaces son abstracciones en la capa de aplicacion
    /// y que su implementacion esta en la infraestructura
    /// </summary>
    public interface IProductQuery
    {
        /// <summary>
        ///  Lista asincronica de productos
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<ICollection<ProductInfo>> ListAsync(string name);
    }
}