using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Orders/{userId}
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == dto.UserId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any())
                return BadRequest("Sepet boş. Sipariş oluşturulamaz.");

            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    SizeId = item.SizeId,
                    Quantity = item.Quantity,
                    PriceAtOrderTime = item.Product!.Price
                });

                var stockItem = await _context.ProductSizes
                    .FirstOrDefaultAsync(ps => ps.ProductId == item.ProductId && ps.SizeId == item.SizeId);

                if (stockItem != null)
                    stockItem.StockQuantity -= item.Quantity;
            }

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        // GET: api/Orders/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetUserOrders(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Size)
                .ToListAsync();

            return orders;
        }

        // GET: api/Orders/5
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Size)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return NotFound();

            return order;
        }
    }
}
