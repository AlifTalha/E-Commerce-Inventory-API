using System;

namespace Ecommerce.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}