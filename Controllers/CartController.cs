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

        // ✅ 1. Sepeti getir (userId üzerinden)
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

        // ✅ 2. Sepete ürün ekle (stok + validasyon kontrolleriyle)
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productSize = await _context.ProductSizes
                .FirstOrDefaultAsync(ps => ps.ProductId == dto.ProductId && ps.SizeId == dto.SizeId);

            if (productSize == null)
                return BadRequest("Bu ürün-beden kombinasyonu sistemde tanımlı değil.");

            if (productSize.StockQuantity <= 0)
                return BadRequest("Bu ürün-beden stoğu tükenmiş.");

            if (dto.Quantity > productSize.StockQuantity)
                return BadRequest($"Stokta yalnızca {productSize.StockQuantity} adet var.");

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
                var item = new CartItem
                {
                    UserId = dto.UserId,
                    ProductId = dto.ProductId,
                    SizeId = dto.SizeId,
                    Quantity = dto.Quantity
                };
                _context.CartItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return Ok("Ürün sepete eklendi.");
        }

        // ✅ 3. Sepet ürününü `CartItem.Id` ile güncelle (klasik yöntem)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuantityById(int id, [FromBody] int quantity)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound();

            item.Quantity = quantity;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ 4. Sepet ürününü `UserId + ProductId + SizeId` ile güncelle (stok kontrolü dahil)
        [HttpPut]
        public async Task<IActionResult> UpdateQuantity([FromBody] CartItemDto dto)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == dto.UserId && c.ProductId == dto.ProductId && c.SizeId == dto.SizeId);

            if (item == null)
                return NotFound("Sepet kaydı bulunamadı.");

            var stock = await _context.ProductSizes
                .FirstOrDefaultAsync(ps => ps.ProductId == dto.ProductId && ps.SizeId == dto.SizeId);

            if (stock == null)
                return BadRequest("Stok bilgisi bulunamadı.");

            if (dto.Quantity > stock.StockQuantity)
                return BadRequest($"Stokta yalnızca {stock.StockQuantity} adet var.");

            item.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();
            return Ok("Sepet güncellendi.");
        }

        // ✅ 5. Sepet ürünü silme (`CartItem.Id` ile)
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCartById(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

        
