using System.ComponentModel.DataAnnotations;

namespace FashionStoreManagement.API.Dtos
{
    public class SizeCreateDto
    {
        [Required(ErrorMessage = "Beden adı boş bırakılamaz.")]
        public string Name { get; set; } = null!;
    }
}
