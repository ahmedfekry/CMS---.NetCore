using CMS.Models.ApiModels.Response;
using CMS.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services
{
    public interface ICategoryService
    {
        public Task<CategoryResponse> CreateCategory(string title,string description = "");
        public Task<CategoryResponse> UpdateCategory();
        public Task<CategoryResponse> DeleteCategory(string id);
        public Task<CategoryResponse> GetCategory(string id);
        public Task<CategoryListResponse> GetCategoryList();

    }
}
