namespace FashionStoreManagement.API.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int SizeId { get; set; }
        public Size? Size { get; set; }

        public int Quantity { get; set; }
    }
}
