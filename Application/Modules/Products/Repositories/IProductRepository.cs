using Application.Modules.Products.Models;

namespace Application.Modules.Products.Repositories
{
    /// <summary>
    /// Abstraccion para el repositorio para operaciones de escritura
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Crea y actualiza un producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ProductInfo> Set(ProductInfo product);

        /// <summary>
        /// Borrar un producto
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<bool> Delete(int productId);
    }
}