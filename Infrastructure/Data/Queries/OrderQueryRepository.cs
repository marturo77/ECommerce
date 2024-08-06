using Application.Modules.Orders.Models;
using Application.Modules.Orders.Queries;
using Business;
using Microsoft.EntityFrameworkCore;

internal class OrderQueryRepository : IOrderQuery
{
    /// <summary>
    ///
    /// </summary>
    private readonly ECommerceContext _context;

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public OrderQueryRepository(ECommerceContext context)
    {
        _context = context;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="customerName"></param>
    /// <returns></returns>
    public async Task<ICollection<OrderInfo>> ListAsync(string? customerName)
    {
        if (string.IsNullOrEmpty(customerName))
        {
            return  await _context.Orders
                 .Include(o => o.OrderItems)
                 .ThenInclude(oi => oi.Product)
                 .ToListAsync();
        }
        else
        {
            return await _context.Orders
                .Where(x => x.CustomerName.ToLower().Contains(customerName.ToLower()))
                                 .ToListAsync();
        }
    }
}