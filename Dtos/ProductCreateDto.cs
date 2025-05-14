using System.ComponentModel.DataAnnotations;

namespace FashionStoreManagement.API.Dtos
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "Ürün adı boş bırakılamaz.")]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Kategori ID zorunludur.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Marka ID zorunludur.")]
        public int BrandId { get; set; }
    }
}
