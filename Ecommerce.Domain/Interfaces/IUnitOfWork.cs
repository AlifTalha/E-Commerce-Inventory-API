using Ecommerce.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        Task<int> CompleteAsync();
    }
}