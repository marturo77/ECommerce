using Application.Modules.Products.Models;
using Application.Modules.Products.Queries;
using Business;

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

    public Task<ICollection<ProductInfo>> ListAsync(string name)
    {
        throw new NotImplementedException();
    }
}