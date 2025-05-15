using FashionStoreManagement.API.Dtos;
using FashionStoreManagement.API.Models;

namespace FashionStoreManagement.API.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(int userId);
        Task AddToCartAsync(CartItemDto dto);
        Task UpdateQuantityAsync(CartItemDto dto);
        Task RemoveFromCartAsync(int userId, int productId, int sizeId);
    }
}
