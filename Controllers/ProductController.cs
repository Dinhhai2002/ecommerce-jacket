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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<StoreProcedureListResult<ProductResponse>>> GetList(
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _productService.GetListAsync(searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<ProductResponse>
            {
                Items = result.Items.Select(p => (ProductResponse)p).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("by-brand/{brandId}")]
        public async Task<ActionResult<StoreProcedureListResult<ProductResponse>>> GetListByBrand(
            int brandId,
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _productService.GetListByBrandAsync(brandId, searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<ProductResponse>
            {
                Items = result.Items.Select(p => (ProductResponse)p).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<ActionResult<StoreProcedureListResult<ProductResponse>>> GetListByCategory(
            int categoryId,
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _productService.GetListByCategoryAsync(categoryId, searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<ProductResponse>
            {
                Items = result.Items.Select(p => (ProductResponse)p).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("by-price-range")]
        public async Task<ActionResult<StoreProcedureListResult<ProductResponse>>> GetListByPriceRange(
            [FromQuery] decimal minPrice,
            [FromQuery] decimal maxPrice,
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _productService.GetListByPriceRangeAsync(minPrice, maxPrice, searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<ProductResponse>
            {
                Items = result.Items.Select(p => (ProductResponse)p).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok((ProductResponse)product);
        }

        [HttpGet("by-sku/{sku}")]
        public async Task<ActionResult<ProductResponse>> GetBySku(string sku)
        {
            var product = await _productService.GetBySkuAsync(sku);
            if (product == null)
                return NotFound();

            return Ok((ProductResponse)product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductResponse>> Create([FromBody] CreateProductRequest request)
        {
            if (await _productService.CheckSkuExistsAsync(request.Sku))
                return BadRequest("Product SKU already exists");

            if (await _productService.CheckNameExistsAsync(request.Name))
                return BadRequest("Product name already exists");

            var brand = await _productService.GetBrandByIdAsync(request.BrandId);
            if (brand == null)
                return BadRequest("Brand not found");

            var category = await _productService.GetCategoryByIdAsync(request.CategoryId);
            if (category == null)
                return BadRequest("Category not found");

            var product = new Product
            {
                Name = request.Name,
                Sku = request.Sku,
                Description = request.Description,
                Price = request.Price,
                DiscountPrice = request.DiscountPrice,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId,
                Status = request.Status
            };

            product = await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, (ProductResponse)product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductResponse>> Update(int id, [FromBody] CreateProductRequest request)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var existingProductBySku = await _productService.GetBySkuAsync(request.Sku);
            if (existingProductBySku != null && existingProductBySku.Id != id)
                return BadRequest("Product SKU already exists");

            var existingProductByName = await _productService.GetByNameAsync(request.Name);
            if (existingProductByName != null && existingProductByName.Id != id)
                return BadRequest("Product name already exists");

            var brand = await _productService.GetBrandByIdAsync(request.BrandId);
            if (brand == null)
                return BadRequest("Brand not found");

            var category = await _productService.GetCategoryByIdAsync(request.CategoryId);
            if (category == null)
                return BadRequest("Category not found");

            product.Name = request.Name;
            product.Sku = request.Sku;
            product.Description = request.Description;
            product.Price = request.Price;
            product.DiscountPrice = request.DiscountPrice;
            product.BrandId = request.BrandId;
            product.CategoryId = request.CategoryId;
            product.Status = request.Status;

            product = await _productService.UpdateAsync(product);
            return Ok((ProductResponse)product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-ids")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetByIds([FromQuery] int[] ids)
        {
            var products = await _productService.GetByIdsAsync(ids);
            return Ok(products.Select(p => (ProductResponse)p));
        }

        [HttpGet("{id}/details")]
        public async Task<ActionResult<ProductDetailResponse>> GetProductDetails(int id)
        {
            var product = await _productService.GetProductDetailsAsync(id);
            if (product == null)
                return NotFound();

            return Ok((ProductDetailResponse)product);
        }

        [HttpGet("{id}/reviews")]
        public async Task<ActionResult<StoreProcedureListResult<ReviewResponse>>> GetProductReviews(
            int id,
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _productService.GetProductReviewsAsync(id, status, pagination);

            return Ok(new StoreProcedureListResult<ReviewResponse>
            {
                Items = result.Items.Select(r => (ReviewResponse)r).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("trending")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetTrendingProducts(
            [FromQuery] int limit = 10)
        {
            var products = await _productService.GetTrendingProductsAsync(limit);
            return Ok(products.Select(p => (ProductResponse)p));
        }

        [HttpGet("new-arrivals")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetNewArrivals(
            [FromQuery] int limit = 10)
        {
            var products = await _productService.GetNewArrivalsAsync(limit);
            return Ok(products.Select(p => (ProductResponse)p));
        }

        [HttpGet("best-sellers")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetBestSellers(
            [FromQuery] int limit = 10)
        {
            var products = await _productService.GetBestSellersAsync(limit);
            return Ok(products.Select(p => (ProductResponse)p));
        }
    }
} 