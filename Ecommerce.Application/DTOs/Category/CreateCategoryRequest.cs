using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}