using FashionStoreManagement.API.Models;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(int userId);
    Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
}
