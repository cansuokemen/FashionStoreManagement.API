using System.ComponentModel.DataAnnotations;

namespace FashionStoreManagement.API.Dtos
{
    public class OrderCreateDto
    {
        [Required(ErrorMessage = "Kullanıcı ID boş bırakılamaz.")]
        public int UserId { get; set; }
    }
}
