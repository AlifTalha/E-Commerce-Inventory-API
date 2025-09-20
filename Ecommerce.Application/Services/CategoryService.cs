using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs.Category;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CreateCategoryRequest request)
        {
            // Check if category name already exists
            var existingCategory = await _unitOfWork.CategoryRepository.FindAsync(c =>
                c.Name == request.Name);

            if (existingCategory.Any())
            {
                throw new Exception("Category with this name already exists");
            }

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            return MapToCategoryResponse(category, 0);
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            var result = categories.Select(category =>
            {
                var productCount = products.Count(p => p.CategoryId == category.Id);
                return MapToCategoryResponse(category, productCount);
            });

            return result;
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

            var products = await _unitOfWork.ProductRepository.FindAsync(p => p.CategoryId == id);
            return MapToCategoryResponse(category, products.Count());
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

          
            var existingCategory = await _unitOfWork.CategoryRepository.FindAsync(c =>
                c.Name == request.Name && c.Id != id);

            if (existingCategory.Any())
            {
                throw new Exception("Category with this name already exists");
            }

            category.Name = request.Name;
            category.Description = request.Description;
            category.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.CompleteAsync();

            var products = await _unitOfWork.ProductRepository.FindAsync(p => p.CategoryId == id);
            return MapToCategoryResponse(category, products.Count());
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }

       
            var products = await _unitOfWork.ProductRepository.FindAsync(p => p.CategoryId == id);
            if (products.Any())
            {
                throw new Exception("Cannot delete category with associated products");
            }

            _unitOfWork.CategoryRepository.Remove(category);
            await _unitOfWork.CompleteAsync();
        }

        private CategoryResponse MapToCategoryResponse(Category category, int productCount)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ProductCount = productCount,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };
        }
    }
}