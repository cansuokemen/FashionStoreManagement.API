using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Dtos;
using Microsoft.EntityFrameworkCore;

public class CartValidator : ICartValidator
{
    private readonly AppDbContext _context;

    public CartValidator(AppDbContext context)
    {
        _context = context;
    }

    public async Task ValidateCartItemAsync(CartItemDto dto)
    {
        var stock = await _context.ProductSizes
            .FirstOrDefaultAsync(ps => ps.ProductId == dto.ProductId && ps.SizeId == dto.SizeId);

        if (stock == null)
            throw new ArgumentException("Ürün-beden kombinasyonu bulunamadı.");

        if (stock.StockQuantity <= 0)
            throw new ArgumentException("Stokta ürün yok.");

        if (dto.Quantity > stock.StockQuantity)
            throw new ArgumentException($"En fazla {stock.StockQuantity} adet eklenebilir.");
    }
}
