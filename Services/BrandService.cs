using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Interfaces;
using FashionStoreManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreManagement.API.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public async Task<Brand> CreateAsync(BrandCreateDto dto)
        {
            var brand = new Brand { Name = dto.Name };
            _context.Brands.Add(brand);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("IX_Brands_Name") == true)
            {
                throw new ArgumentException("Bu marka adı zaten kayıtlı.");
            }

            return brand;
        }

        public async Task<bool> UpdateAsync(int id, Brand brand)
        {
            if (id != brand.Id)
                return false;

            _context.Entry(brand).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
                return false;

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
