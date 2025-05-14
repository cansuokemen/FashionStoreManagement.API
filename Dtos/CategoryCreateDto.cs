using System.ComponentModel.DataAnnotations;

namespace FashionStoreManagement.API.Dtos
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Kategori adı boş bırakılamaz.")]
        public string Name { get; set; } = null!;
    }
}
