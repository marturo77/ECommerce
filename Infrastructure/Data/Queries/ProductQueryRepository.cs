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
    private readonly ICachingManager _cacheManager;

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public ProductQueryRepository(ECommerceContext context, ICachingManager cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
    }

    /// <summary>
    /// Lista de datos de un cache para un escenario donde la cantidad de informacion no es lo suficientemente enorme
    /// para optar por otra solucion
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<ICollection<ProductInfo>> ListAsync(string? name)
    {
        // Obtiene los datos del cache
        ICollection<ProductInfo>? list = _cacheManager.GetData<ICollection<ProductInfo>>(CachingManager.ProductList);

        // Si no hay cache obtiene los datos del repositorio
        if (list == null)
        {
            // Obtiene los datos y los guarda en el cache
            list = await _context.Products.ToListAsync();
            _cacheManager.SetStandard(CachingManager.ProductList, list);
        }

        // Aplica el filtro
        if (!string.IsNullOrEmpty(name))
        {
            list = list
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        return list ?? new List<ProductInfo>();
    }
}