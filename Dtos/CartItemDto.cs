using System.ComponentModel.DataAnnotations;

namespace FashionStoreManagement.API.Dtos
{
    public class CartItemDto
    {
        [Required(ErrorMessage = "Kullanıcı ID boş bırakılamaz.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Ürün ID boş bırakılamaz.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Beden ID boş bırakılamaz.")]
        public int SizeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Miktar en az 1 olmalıdır.")]
        public int Quantity { get; set; }
    }
}
