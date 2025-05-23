using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Entities;
using FashionStoreManagement.API.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreManagement.API.Services.Concrete
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly AppDbContext _context;

        public ProductSizeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductSize>> GetAllAsync()
        {
            return await _context.ProductSizes
                .Include(ps => ps.Product)
                .Include(ps => ps.Size)
                .ToListAsync();
        }

        public async Task<ProductSize?> GetByIdsAsync(int productId, int sizeId)
        {
            return await _context.ProductSizes
                .Include(ps => ps.Product)
                .Include(ps => ps.Size)
                .FirstOrDefaultAsync(ps => ps.ProductId == productId && ps.SizeId == sizeId);
        }

        public async Task<ProductSize> CreateAsync(ProductSize productSize)
        {
            _context.ProductSizes.Add(productSize);
            await _context.SaveChangesAsync();
            return productSize;
        }

        public async Task<bool> DeleteAsync(int productId, int sizeId)
        {
            var item = await _context.ProductSizes
                .FirstOrDefaultAsync(ps => ps.ProductId == productId && ps.SizeId == sizeId);

            if (item == null) return false;

            _context.ProductSizes.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
