using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;
using webecommerce.Common.Utils;
using System.Collections.Generic;
using System;

namespace webecommerce.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IFirebaseImageService _firebaseImageService;

        public CategoryController(
            ICategoryService categoryService,
            IFirebaseImageService firebaseImageService)
        {
            _categoryService = categoryService;
            _firebaseImageService = firebaseImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int parentId = -1,
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _categoryService.GetList(parentId, keySearch, status, pagination);

                var listData = new BaseListDataResponse<CategoryResponse>
                {
                    List = result.Data.Select(c => new CategoryResponse { Category = c }).ToList(),
                    TotalRecord = result.TotalRecord
                };

                return OkWithData(listData);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var pagination = new Pagination(int.MaxValue, 0);
                var result = await _categoryService.GetList(-1, "", 1, pagination);

                var categoryResponses = result.Data.Select(c => new CategoryResponse { Category = c }).ToList();
                return OkWithData(categoryResponses);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                if (category == null)
                    return BadRequestWithMessage("Category not found");

                return OkWithData(new CategoryResponse { Category = category });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("{id}/change-status")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                if (category == null)
                    return BadRequestWithMessage("Category not found");

                category.Status = category.Status == 1 ? 0 : 1;
                await _categoryService.Update(category);

                return OkWithData(new CategoryResponse { Category = category });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDCategoryRequest request)
        {
            try
            {
                var existingCategory = await _categoryService.GetByName(request.Name);
                if (existingCategory != null)
                    return BadRequestWithMessage("Category already exists");

                var category = new Category
                {
                    Name = request.Name,
                    ParentId = request.ParentId,
                    ImageUrl = request.ImageUrl,
                    Status = 1
                };

                await _categoryService.Create(category);
                return OkWithData(new CategoryResponse { Category = category });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDCategoryRequest request)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                if (category == null)
                    return BadRequestWithMessage("Category not found");

                if (category.Name != request.Name)
                {
                    var existingCategory = await _categoryService.GetByName(request.Name);
                    if (existingCategory != null)
                        return BadRequestWithMessage("Category name already exists");
                }

                category.Name = request.Name;
                category.ParentId = request.ParentId;
                category.ImageUrl = request.ImageUrl;

                await _categoryService.Update(category);
                return OkWithData(new CategoryResponse { Category = category });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                if (category == null)
                    return BadRequestWithMessage("Category not found");

                var fileName = await _firebaseImageService.SaveAsync(file);
                var imageUrl = await _firebaseImageService.GetImageUrlAsync(fileName);

                category.ImageUrl = imageUrl;
                await _categoryService.Update(category);

                return OkWithData(new CategoryResponse { Category = category });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 