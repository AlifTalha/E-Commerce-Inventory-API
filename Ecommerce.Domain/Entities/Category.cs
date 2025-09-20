using System;
using System.Collections.Generic;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}