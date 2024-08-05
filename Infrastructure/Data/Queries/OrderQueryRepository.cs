using Application.Modules.Orders.Models;
using Application.Modules.Orders.Queries;
using Business;

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

    public Task<ICollection<OrderInfo>> ListAsync(string customerName)
    {
        throw new NotImplementedException();
    }
}