using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}