using Microsoft.EntityFrameworkCore;

namespace FashionStoreManagement.API.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int SizeId { get; set; }
        public Size? Size { get; set; }

        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal PriceAtOrderTime { get; set; }
    }
}
