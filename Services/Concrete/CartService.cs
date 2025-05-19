namespace FashionStoreManagement.API.Services.Concrete;
using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Entities;
using FashionStoreManagement.API.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

public class CartService : ICartService
{
    private readonly AppDbContext _context;
    private readonly ICartValidator _validator;

    public CartService(AppDbContext context, ICartValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsAsync(int userId)
    {
        return await _context.CartItems
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .Include(c => c.Size)
            .ToListAsync();
    }

    public async Task AddToCartAsync(CartItemDto dto)
    {
        await _validator.ValidateCartItemAsync(dto);

        var existing = await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == dto.UserId && c.ProductId == dto.ProductId && c.SizeId == dto.SizeId);

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
    }

    public async Task UpdateQuantityAsync(CartItemDto dto)
    {
        await _validator.ValidateCartItemAsync(dto);

        var item = await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == dto.UserId && c.ProductId == dto.ProductId && c.SizeId == dto.SizeId);

        if (item == null) throw new ArgumentException("Sepet kaydı bulunamadı.");

        item.Quantity = dto.Quantity;
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromCartAsync(int userId, int productId, int sizeId)
    {
        var item = await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId && c.SizeId == sizeId);

        if (item != null)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
