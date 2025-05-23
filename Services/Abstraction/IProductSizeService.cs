using FashionStoreManagement.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionStoreManagement.API.Services.Abstraction
{
    public interface IProductSizeService
    {
        Task<IEnumerable<ProductSize>> GetAllAsync();
        Task<ProductSize?> GetByIdsAsync(int productId, int sizeId);
        Task<ProductSize> CreateAsync(ProductSize productSize);
        Task<bool> DeleteAsync(int productId, int sizeId);
    }
}
