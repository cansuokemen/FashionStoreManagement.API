using FashionStoreManagement.API.Dtos;

public interface ICartValidator
{
    Task ValidateCartItemAsync(CartItemDto dto);
}
