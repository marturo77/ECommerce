using Application.Modules.Products.Models;
using Application.Modules.Products.Queries;
using Business;
using Microsoft.EntityFrameworkCore;

internal class ProductQueryRepository : IProductQuery
{
    /// <summary>
    ///
    /// </summary>
    private readonly ECommerceContext _context;

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public ProductQueryRepository(ECommerceContext context)
    {
        _context = context;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<ICollection<ProductInfo>> ListAsync(string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return await _context.Products.ToListAsync();
        }
        else
        {
            return await _context.Products
                 .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                 .ToListAsync();
        }
    }
}