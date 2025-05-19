using FashionStoreManagement.API.Entities;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(int userId);
    Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
}
