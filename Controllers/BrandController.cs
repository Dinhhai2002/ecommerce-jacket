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
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult<StoreProcedureListResult<BrandResponse>>> GetList(
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _brandService.GetListAsync(searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<BrandResponse>
            {
                Items = result.Items.Select(b => (BrandResponse)b).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("with-products")]
        public async Task<ActionResult<StoreProcedureListResult<BrandResponse>>> GetListWithProducts(
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _brandService.GetListWithProductsAsync(searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<BrandResponse>
            {
                Items = result.Items.Select(b => (BrandResponse)b).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandResponse>> GetById(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
                return NotFound();

            return Ok((BrandResponse)brand);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BrandResponse>> Create([FromBody] CreateBrandRequest request)
        {
            if (await _brandService.CheckNameExistsAsync(request.Name))
                return BadRequest("Brand name already exists");

            var brand = new Brand
            {
                Name = request.Name,
                Description = request.Description,
                Logo = request.Logo,
                Status = request.Status
            };

            brand = await _brandService.CreateAsync(brand);
            return CreatedAtAction(nameof(GetById), new { id = brand.Id }, (BrandResponse)brand);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BrandResponse>> Update(int id, [FromBody] CreateBrandRequest request)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
                return NotFound();

            var existingBrand = await _brandService.GetByNameAsync(request.Name);
            if (existingBrand != null && existingBrand.Id != id)
                return BadRequest("Brand name already exists");

            brand.Name = request.Name;
            brand.Description = request.Description;
            brand.Logo = request.Logo;
            brand.Status = request.Status;

            brand = await _brandService.UpdateAsync(brand);
            return Ok((BrandResponse)brand);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _brandService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-ids")]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetByIds([FromQuery] int[] ids)
        {
            var brands = await _brandService.GetByIdsWithProductsAsync(ids);
            return Ok(brands.Select(b => (BrandResponse)b));
        }
    }
} 