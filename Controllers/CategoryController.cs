using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Common.Utils;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;

namespace webecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<StoreProcedureListResult<CategoryResponse>>> GetList(
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _categoryService.GetListAsync(searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<CategoryResponse>
            {
                Items = result.Items.Select(c => (CategoryResponse)c).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("with-products")]
        public async Task<ActionResult<StoreProcedureListResult<CategoryResponse>>> GetListWithProducts(
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _categoryService.GetListWithProductsAsync(searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<CategoryResponse>
            {
                Items = result.Items.Select(c => (CategoryResponse)c).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("root")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetRootCategories()
        {
            var categories = await _categoryService.GetRootCategoriesAsync();
            return Ok(categories.Select(c => (CategoryResponse)c));
        }

        [HttpGet("children/{parentId}")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetChildren(int parentId)
        {
            var categories = await _categoryService.GetChildrenAsync(parentId);
            return Ok(categories.Select(c => (CategoryResponse)c));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok((CategoryResponse)category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CreateCategoryRequest request)
        {
            if (await _categoryService.CheckNameExistsAsync(request.Name))
                return BadRequest("Category name already exists");

            if (request.ParentId.HasValue)
            {
                var parentCategory = await _categoryService.GetByIdAsync(request.ParentId.Value);
                if (parentCategory == null)
                    return BadRequest("Parent category not found");
            }

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                ParentId = request.ParentId,
                ImageUrl = request.ImageUrl,
                Status = request.Status
            };

            category = await _categoryService.CreateAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, (CategoryResponse)category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryResponse>> Update(int id, [FromBody] CreateCategoryRequest request)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var existingCategory = await _categoryService.GetByNameAsync(request.Name);
            if (existingCategory != null && existingCategory.Id != id)
                return BadRequest("Category name already exists");

            if (request.ParentId.HasValue)
            {
                if (request.ParentId.Value == id)
                    return BadRequest("Category cannot be its own parent");

                var parentCategory = await _categoryService.GetByIdAsync(request.ParentId.Value);
                if (parentCategory == null)
                    return BadRequest("Parent category not found");

                // Check if the new parent is not a descendant of the current category
                var descendants = await GetDescendantIds(id);
                if (descendants.Contains(request.ParentId.Value))
                    return BadRequest("Cannot move a category to its own descendant");
            }

            category.Name = request.Name;
            category.Description = request.Description;
            category.ParentId = request.ParentId;
            category.ImageUrl = request.ImageUrl;
            category.Status = request.Status;

            category = await _categoryService.UpdateAsync(category);
            return Ok((CategoryResponse)category);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            // Check if category has children
            var children = await _categoryService.GetChildrenAsync(id);
            if (children.Any())
                return BadRequest("Cannot delete category with children. Delete children first.");

            var result = await _categoryService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-ids")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetByIds([FromQuery] int[] ids)
        {
            var categories = await _categoryService.GetByIdsWithProductsAsync(ids);
            return Ok(categories.Select(c => (CategoryResponse)c));
        }

        private async Task<HashSet<int>> GetDescendantIds(int categoryId)
        {
            var result = new HashSet<int>();
            var children = await _categoryService.GetChildrenAsync(categoryId);
            
            foreach (var child in children)
            {
                result.Add(child.Id);
                var descendants = await GetDescendantIds(child.Id);
                result.UnionWith(descendants);
            }

            return result;
        }
    }
} 