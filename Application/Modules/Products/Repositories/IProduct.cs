using Application.Modules.Products.Models;

namespace Application.Modules.Products.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Crea y actualiza un producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<ProductInfo> Set(ProductInfo product);
    }
}