using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs.Product;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponse> CreateProductAsync(CreateProductRequest request, Guid userId)
        {
            // Check if category exists
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl,
                CreatedBy = userId
            };

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            return MapToProductResponse(product, category);
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(
            Guid? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int page = 1,
            int limit = 10)
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            var query = from p in products
                        join c in categories on p.CategoryId equals c.Id
                        select new { Product = p, Category = c };

            
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.Product.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(x => x.Product.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(x => x.Product.Price <= maxPrice.Value);
            }

           
            var result = query
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(x => MapToProductResponse(x.Product, x.Category))
                .ToList();

            return result;
        }

        public async Task<ProductResponse> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(product.CategoryId);
            return MapToProductResponse(product, category);
        }

        public async Task<ProductResponse> UpdateProductAsync(Guid id, UpdateProductRequest request)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

          
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.CategoryId = request.CategoryId;
            product.ImageUrl = request.ImageUrl;
            product.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.CompleteAsync();

            return MapToProductResponse(product, category);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            _unitOfWork.ProductRepository.Remove(product);
            await _unitOfWork.CompleteAsync();
        }

        private ProductResponse MapToProductResponse(Product product, Category category)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = category?.Name,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }
    }
}