using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Entities;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category> CreateAsync(CategoryCreateDto dto);
    Task<bool> UpdateAsync(int id, Category category);
    Task<bool> DeleteAsync(int id);
}
