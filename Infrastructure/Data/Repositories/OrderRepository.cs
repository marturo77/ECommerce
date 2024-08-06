using Application.Modules.Orders.Models;
using Application.Modules.Orders.Repositories;
using Business;
using Microsoft.EntityFrameworkCore;

internal class OrderRepository : IOrderRepository
{
    private readonly ECommerceContext _context;

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
            var existingOrder = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            if (existingOrder != null)
            {
                existingOrder.CustomerName = order.CustomerName;
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.Status = order.Status;
                existingOrder.Total = order.Total;

                // Actualizar los OrderItems
                var existingOrderItemIds = existingOrder.OrderItems.Select(oi => oi.OrderItemId).ToList();
                var newOrderItemIds = order.OrderItems.Select(oi => oi.OrderItemId).ToList();

                // Eliminar los OrderItems que no están en la nueva lista
                foreach (var itemId in existingOrderItemIds)
                {
                    if (!newOrderItemIds.Contains(itemId))
                    {
                        var itemToRemove = existingOrder.OrderItems.First(oi => oi.OrderItemId == itemId);
                        _context.OrderItems.Remove(itemToRemove);
                    }
                }

                // Agregar o actualizar los OrderItems
                foreach (var item in order.OrderItems)
                {
                    var existingItem = existingOrder.OrderItems.FirstOrDefault(oi => oi.OrderItemId == item.OrderItemId);
                    if (existingItem != null)
                    {
                        existingItem.Price = item.Price;
                        existingItem.ProductId = item.ProductId;
                        existingItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        existingOrder.OrderItems.Add(new OrderItemInfo
                        {
                            OrderId = existingOrder.OrderId,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        });
                    }
                }

                _context.Orders.Update(existingOrder);
            }
        }
        else
        {
            OrderInfo newOrder = _context.Orders.Add(new OrderInfo
            {
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                Status = order.Status,
                Total = order.Total,
                OrderItems = order.OrderItems.Select(item => new OrderItemInfo
                {
                    Price = item.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            }).Entity;
        }

        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<bool> Delete(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(x => x.OrderId == orderId);

        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
