using System.ComponentModel.DataAnnotations;

namespace FashionStoreManagement.API.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "E-posta boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        public string Password { get; set; } = null!;
    }
}
