using System;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs.Auth;
using Ecommerce.Application.Interface;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse> RegisterAsync(RegisterRequest request)
        {
            // Check if user already exists
            var existingUsers = await _unitOfWork.UserRepository.FindAsync(u =>
                u.Email == request.Email || u.Username == request.Username);

            if (existingUsers.Any())
            {
                throw new Exception("User with this email or username already exists");
            }

          
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password)
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            // Generate token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var users = await _unitOfWork.UserRepository.FindAsync(u => u.Email == request.Email);
            var existingUser = users.FirstOrDefault();

            if (existingUser == null)
            {
                throw new Exception("Invalid email or password");
            }

         
            var isPasswordValid = _passwordHasher.VerifyPassword(existingUser.PasswordHash, request.Password);

            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password");
            }

          
            var token = _jwtTokenGenerator.GenerateToken(existingUser);

            return new LoginResponse
            {
                Token = token,
                Username = existingUser.Username,
                Email = existingUser.Email
            };
        }
    }
}