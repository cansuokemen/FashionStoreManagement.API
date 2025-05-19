using Microsoft.EntityFrameworkCore;

namespace FashionStoreManagement.API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        public ICollection<ProductSize>? ProductSizes { get; set; }
    }
}
