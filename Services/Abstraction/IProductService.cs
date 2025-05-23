using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionStoreManagement.API.Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(ProductCreateDto dto);
        Task<Product?> UpdateAsync(int id, ProductCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
