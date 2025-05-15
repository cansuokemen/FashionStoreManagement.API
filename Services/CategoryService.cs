using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Models;
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;
    public CategoryService(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

    public async Task<Category?> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);

    public async Task<Category> CreateAsync(CategoryCreateDto dto)
    {
        var category = new Category { Name = dto.Name };
        _context.Categories.Add(category);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("IX_Categories_Name") == true)
        {
            throw new ArgumentException("Bu kategori adı zaten kayıtlı.");
        }

        return category;
    }

    public async Task<bool> UpdateAsync(int id, Category category)
    {
        if (id != category.Id) return false;
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        bool hasProducts = await _context.Products.AnyAsync(p => p.CategoryId == id);
        if (hasProducts)
            throw new InvalidOperationException("Bu kategoriye ait ürünler olduğu için silinemez.");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
