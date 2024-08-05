using Application.Modules.Orders.Models;
using Application.Modules.Orders.Repositories;
using Business;

internal class OrderRepository : IOrderRepository
{
    /// <summary>
    ///
    /// </summary>
    private readonly ECommerceContext _context;

    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    public OrderRepository(ECommerceContext context)
    {
        _context = context;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public async Task<OrderInfo> Set(OrderInfo order)
    {
        if (order.OrderId > 0)
        {
            _context.Orders.Update(order);
        }
        else
        {
            _context.Orders.Add(order);
        }
        await _context.SaveChangesAsync();
        return order;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public async Task<bool> Delete(int orderId)
    {
        OrderInfo? order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);

        if (order is not null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}