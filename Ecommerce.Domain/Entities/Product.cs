using System;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public virtual Category Category { get; set; }
        public virtual User Creator { get; set; }
    }
}