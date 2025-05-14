using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cart/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCart(int userId)
        {
            var items = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .Include(c => c.Size)
                .ToListAsync();

            return items;
        }

        // POST: api/Cart
        [HttpPost]
        public async Task<ActionResult<CartItem>> AddToCart([FromBody] CartItemDto dto)
        {
            var existing = await _context.CartItems
                .FirstOrDefaultAsync(c =>
                    c.UserId == dto.UserId &&
                    c.ProductId == dto.ProductId &&
                    c.SizeId == dto.SizeId);

            if (existing != null)
            {
                existing.Quantity += dto.Quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    UserId = dto.UserId,
                    ProductId = dto.ProductId,
                    SizeId = dto.SizeId,
                    Quantity = dto.Quantity
                };
                _context.CartItems.Add(newItem);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }


        // PUT: api/Cart/{cartItemId}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuantity(int id, [FromBody] int quantity)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound();

            item.Quantity = quantity;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cart/{cartItemId}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
