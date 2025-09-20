using System;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;

namespace Ecommerce.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<Category> _categoryRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<User> UserRepository =>
            _userRepository ??= new GenericRepository<User>(_context);

        public IGenericRepository<Product> ProductRepository =>
            _productRepository ??= new GenericRepository<Product>(_context);

        public IGenericRepository<Category> CategoryRepository =>
            _categoryRepository ??= new GenericRepository<Category>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}