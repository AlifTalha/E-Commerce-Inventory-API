using System.Threading.Tasks;
using Ecommerce.Application.DTOs.Auth;

namespace Ecommerce.Application.Interface
{
    public interface IAuthService
    {
        Task<LoginResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}