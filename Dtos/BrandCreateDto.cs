using System.ComponentModel.DataAnnotations;

namespace FashionStoreManagement.API.Dtos
{
    public class BrandCreateDto
    {
        [Required(ErrorMessage = "Marka adı boş bırakılamaz.")]
        public string Name { get; set; } = null!;
    }
}
