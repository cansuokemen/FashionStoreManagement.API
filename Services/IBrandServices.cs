using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Models;

public interface IBrandService
{
    Task<IEnumerable<Brand>> GetAllAsync();
    Task<Brand?> GetByIdAsync(int id);
    Task<Brand> CreateAsync(BrandCreateDto dto);
    Task<bool> UpdateAsync(int id, Brand brand);
    Task<bool> DeleteAsync(int id);
}

