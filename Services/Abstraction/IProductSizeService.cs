using FashionStoreManagement.API.Entities;
using FashionStoreManagement.API.Dtos;

public interface IProductSizeService
{
    Task<IEnumerable<ProductSize>> GetAllAsync();
    Task<ProductSize?> GetByIdsAsync(int productId, int sizeId);
    Task<ProductSize> CreateAsync(ProductSizeCreateDto dto);
    Task<bool> DeleteAsync(int productId, int sizeId);
}
