using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FashionStoreManagement.API.Services.Abstraction
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(int id, User user);
        Task<bool> DeleteAsync(int id);
        Task<User> RegisterAsync(UserRegisterDto dto);
        Task<User?> LoginAsync(UserLoginDto dto);


    }
}
