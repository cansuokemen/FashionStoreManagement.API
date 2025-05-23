using FashionStoreManagement.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionStoreManagement.API.Services.Abstraction
{
    public interface ISizeService
    {
        Task<IEnumerable<Size>> GetAllAsync();
        Task<Size?> GetByIdAsync(int id);
    }
}
