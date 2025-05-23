using FashionStoreManagement.API.Data;
using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Entities;
using FashionStoreManagement.API.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreManagement.API.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(int id, User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> RegisterAsync(UserRegisterDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = dto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> LoginAsync(UserLoginDto dto)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);
        }

    }
}
