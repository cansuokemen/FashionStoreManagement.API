using System.ComponentModel.DataAnnotations;

namespace FashionStoreManagement.API.Dtos
{
    public class ProductSizeCreateDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int SizeId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz.")]
        public int StockQuantity { get; set; }
    }
}
