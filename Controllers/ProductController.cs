using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;
using webecommerce.Common.Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace webecommerce.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IImageService _imageService;
        private readonly IFirebaseImageService _firebaseImageService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IBrandService brandService,
            IImageService imageService,
            IFirebaseImageService firebaseImageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _imageService = imageService;
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
                var result = await _productService.GetList(keySearch, status, pagination);

                // Get unique brandIds and categoryIds
                var brandIds = result.Data.Select(p => p.BrandId).Distinct().ToList();
                var categoryIds = result.Data.Select(p => p.CategoryId).Distinct().ToList();

                // Get brands and categories
                var brands = await _brandService.GetByIds(brandIds);
                var categories = await _categoryService.GetByIds(categoryIds);

                // Create lookup dictionaries
                var brandDict = brands.ToDictionary(b => b.Id);
                var categoryDict = categories.ToDictionary(c => c.Id);

                // Map to response
                var productResponses = result.Data.Select(product => new ProductResponse
                {
                    Product = product,
                    BrandName = brandDict.ContainsKey(product.BrandId) ? brandDict[product.BrandId].Name : null,
                    CategoryName = categoryDict.ContainsKey(product.CategoryId) ? categoryDict[product.CategoryId].Name : null
                }).ToList();

                return OkWithData(new BaseListDataResponse<ProductResponse>
                {
                    List = productResponses,
                    TotalRecord = result.TotalRecord
                });
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
                var product = await _productService.GetById(id);
                if (product == null)
                    return BadRequestWithMessage("Product not found");

                var brand = await _brandService.GetById(product.BrandId);
                var category = await _categoryService.GetById(product.CategoryId);

                var response = new ProductResponse
                {
                    Product = product,
                    BrandName = brand?.Name,
                    CategoryName = category?.Name
                };

                return OkWithData(response);
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
                var product = await _productService.GetById(id);
                if (product == null)
                    return BadRequestWithMessage("Product not found");

                product.Status = product.Status == 1 ? 0 : 1;
                await _productService.Update(product);

                return OkWithData(new ProductResponse { Product = product });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDProductRequest request)
        {
            try
            {
                var existingProduct = await _productService.GetByName(request.Name);
                if (existingProduct != null)
                    return BadRequestWithMessage("Product already exists");

                var category = await _categoryService.GetById(request.CategoryId);
                if (category == null)
                    return BadRequestWithMessage("Category not found");

                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description ?? "",
                    BrandId = request.BrandId,
                    CategoryId = request.CategoryId,
                    Price = request.Price,
                    AverageRating = 0,
                    Status = 1
                };

                await _productService.Create(product);
                return OkWithData(new ProductResponse { Product = product });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDProductRequest request)
        {
            try
            {
                var product = await _productService.GetById(id);
                if (product == null)
                    return BadRequestWithMessage("Product not found");

                if (product.Name != request.Name)
                {
                    var existingProduct = await _productService.GetByName(request.Name);
                    if (existingProduct != null)
                        return BadRequestWithMessage("Product name already exists");
                }

                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;

                await _productService.Update(product);
                return OkWithData(new ProductResponse { Product = product });
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
                var product = await _productService.GetById(id);
                if (product == null)
                    return BadRequestWithMessage("Product not found");

                var fileName = await _firebaseImageService.SaveAsync(file);
                var imageUrl = await _firebaseImageService.GetImageUrlAsync(fileName);

                var image = new Image
                {
                    Url = imageUrl,
                    ProductId = id
                };
                await _imageService.Create(image);

                product.ImageUrl = imageUrl;
                await _productService.Update(product);

                return OkWithData(new ProductResponse { Product = product });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("{id}/images")]
        public async Task<IActionResult> UploadMultipleImages(int id, List<IFormFile> files)
        {
            try
            {
                var product = await _productService.GetById(id);
                if (product == null)
                    return BadRequestWithMessage("Product not found");

                var images = new List<Image>();
                foreach (var file in files)
                {
                    var fileName = await _firebaseImageService.SaveAsync(file);
                    var imageUrl = await _firebaseImageService.GetImageUrlAsync(fileName);

                    var image = new Image
                    {
                        Url = imageUrl,
                        ProductId = id
                    };
                    await _imageService.Create(image);
                    images.Add(image);
                }

                if (images.Any())
                {
                    product.ImageUrl = images.First().Url;
                    await _productService.Update(product);
                }

                var productResponses = images.Select(_ => new ProductResponse { Product = product }).ToList();
                return OkWithData(productResponses);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 