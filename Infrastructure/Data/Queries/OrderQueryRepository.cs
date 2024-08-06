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

    public async Task<ICollection<OrderInfo>> ListAsync(string customerName)
    {
        return await _context.Orders
            .Where(x => x.Customer.ToLower().Contains(customerName.ToLower()))
            .ToListAsync();
    }
}