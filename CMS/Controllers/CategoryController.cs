using CMS.Core.Repositories;
using CMS.Core.Services;
using CMS.Models.ApiModels.Request;
using CMS.Models.ApiModels.Response;
using CMS.Models.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Admin")]
        public async Task<CategoryResponse> Create(CategoryRequest categoryRequest)
        {
            CategoryResponse categoryResponse =await _categoryService.CreateCategory(categoryRequest.Title, categoryRequest.Description);
            
            return categoryResponse;
        }

        [HttpGet]
        public async Task<CategoryListResponse> Index()
        {
            return await _categoryService.GetCategoryList();
        }
    }
}
