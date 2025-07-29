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
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;
        private readonly IFirebaseImageService _firebaseImageService;

        public BrandController(
            IBrandService brandService,
            IFirebaseImageService firebaseImageService)
        {
            _brandService = brandService;
            _firebaseImageService = firebaseImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _brandService.GetList(keySearch, status, pagination);

                var listData = new BaseListDataResponse<BrandResponse>
                {
                    List = result.Data.Select(b => new BrandResponse { Brand = b }).ToList(),
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
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var pagination = new Pagination(int.MaxValue, 0);
                var result = await _brandService.GetList("", 1, pagination);

                var brandResponses = result.Data.Select(b => new BrandResponse { Brand = b }).ToList();
                return OkWithData(brandResponses);
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
                var brand = await _brandService.GetById(id);
                if (brand == null)
                    return BadRequestWithMessage("Brand not found");

                return OkWithData(new BrandResponse { Brand = brand });
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
                var brand = await _brandService.GetById(id);
                if (brand == null)
                    return BadRequestWithMessage("Brand not found");

                brand.Status = brand.Status == 1 ? 0 : 1;
                await _brandService.Update(brand);

                return OkWithData(new BrandResponse { Brand = brand });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDBrandRequest request)
        {
            try
            {
                var existingBrand = await _brandService.GetByName(request.Name);
                if (existingBrand != null)
                    return BadRequestWithMessage("Brand already exists");

                var brand = new Brand
                {
                    Name = request.Name,
                    ImageUrl = request.ImageUrl,
                    Status = 1
                };

                await _brandService.Create(brand);
                return OkWithData(new BrandResponse { Brand = brand });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDBrandRequest request)
        {
            try
            {
                var brand = await _brandService.GetById(id);
                if (brand == null)
                    return BadRequestWithMessage("Brand not found");

                if (brand.Name != request.Name)
                {
                    var existingBrand = await _brandService.GetByName(request.Name);
                    if (existingBrand != null)
                        return BadRequestWithMessage("Brand name already exists");
                }

                brand.Name = request.Name;
                brand.ImageUrl = request.ImageUrl;

                await _brandService.Update(brand);
                return OkWithData(new BrandResponse { Brand = brand });
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
                var brand = await _brandService.GetById(id);
                if (brand == null)
                    return BadRequestWithMessage("Brand not found");

                var fileName = await _firebaseImageService.SaveAsync(file);
                var imageUrl = await _firebaseImageService.GetImageUrlAsync(fileName);

                brand.ImageUrl = imageUrl;
                await _brandService.Update(brand);

                return OkWithData(new BrandResponse { Brand = brand });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 