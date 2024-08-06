using Application.Modules.Products.Models;
using Application.Modules.Products.Repositories;
using Business;
using Microsoft.EntityFrameworkCore;

internal class ProductRepository : IProductRepository
{
    /// <summary>
    ///
    /// </summary>
    private readonly ECommerceContext _context;

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public ProductRepository(ECommerceContext context)
    {
        _context = context;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public async Task<ProductInfo> Set(ProductInfo product)
    {
        bool exist = await ProductExistAsync(product.Name);

        if (exist && product.ProductId > 0)
        {
            _context.Products.Update(product);
        }
        else
        {
            _context.Products.Add(product);
        }
        await _context.SaveChangesAsync();
        return product;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public async Task<bool> Delete(int productId)
    {
        ProductInfo? product = _context.Products.FirstOrDefault(x => x.ProductId == productId);

        if (product is not null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<bool> ProductExistAsync(string name) =>
      await _context.Products.AnyAsync(x => x.Name.ToLower() == name.ToLower());
}