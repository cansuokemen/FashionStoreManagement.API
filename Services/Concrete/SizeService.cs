using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Entities;
using FashionStoreManagement.API.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreManagement.API.Services.Concrete
{
    public class SizeService : ISizeService
    {
        private readonly AppDbContext _context;

        public SizeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Size>> GetAllAsync()
        {
            return await _context.Sizes.ToListAsync();
        }

        public async Task<Size?> GetByIdAsync(int id)
        {
            return await _context.Sizes.FindAsync(id);
        }
    }
}
