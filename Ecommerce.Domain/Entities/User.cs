using System;
using System.Collections.Generic;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

     
        public virtual ICollection<Product> Products { get; set; }
    }
}