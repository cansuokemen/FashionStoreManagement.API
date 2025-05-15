using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Models;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(int userId)
    {
        var cartItems = await _context.CartItems
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        if (!cartItems.Any())
            throw new InvalidOperationException("Sepet boş. Sipariş oluşturulamaz.");

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            OrderItems = cartItems.Select(c => new OrderItem
            {
                ProductId = c.ProductId,
                SizeId = c.SizeId,
                Quantity = c.Quantity,
                PriceAtOrderTime = c.Product!.Price

            }).ToList()
        };

        _context.Orders.Add(order);
        _context.CartItems.RemoveRange(cartItems);

        foreach (var item in order.OrderItems)
        {
            var stock = await _context.ProductSizes
                .FirstOrDefaultAsync(ps => ps.ProductId == item.ProductId && ps.SizeId == item.SizeId);

            if (stock != null)
            {
                stock.StockQuantity -= item.Quantity;
            }
        }

        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
            .ToListAsync();
    }
}
