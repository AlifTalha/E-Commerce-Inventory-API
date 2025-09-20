using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs.Product
{
    public class CreateProductRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        public string ImageUrl { get; set; }
    }
}