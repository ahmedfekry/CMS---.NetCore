using CMS.Core.Repositories;
using CMS.Core.Services;
using CMS.Models.ApiModels.Response;
using CMS.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponse> CreateCategory(string title, string description = "")
        {
            CategoryResponse categoryResponse = new CategoryResponse();

            var category = await _unitOfWork.CategoryRepository.Get( cat => cat.Title.Equals(title));

            if (category != null)
            {
                categoryResponse.Message = "Category with that name already exists";
                categoryResponse.Status = false;
                return categoryResponse;
            }

            category = new Category()
            {
                Title = title,
                Description = description
            };

            category = _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.SaveChanges();

            categoryResponse.Message = "Category Created Successfuly";
            categoryResponse.Status = true;
            categoryResponse.Category = category;

            return categoryResponse;
        }

        public Task<CategoryResponse> DeleteCategory(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryListResponse> GetCategory()
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse> GetCategory(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryListResponse> GetCategoryList()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            
            CategoryListResponse categoryListResponse = new CategoryListResponse();
            categoryListResponse.Status = true;
            categoryListResponse.Message = string.Empty;
            categoryListResponse.Categories = categories.ToList();

            return categoryListResponse;
        }

        public Task<CategoryResponse> UpdateCategory()
        {
            throw new NotImplementedException();
        }
    }
}
