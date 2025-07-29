using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;
using webecommerce.Common.Utils;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace webecommerce.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductDetailController : BaseController
    {
        private readonly IProductDetailService _productDetailService;
        private readonly IProductService _productService;
        private readonly IFirebaseImageService _firebaseImageService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private readonly IMaterialsService _materialsService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        public ProductDetailController(
            IProductDetailService productDetailService,
            IProductService productService,
            IFirebaseImageService firebaseImageService,
            IColorService colorService,
            ISizeService sizeService,
            IMaterialsService materialsService,
            IBrandService brandService,
            ICategoryService categoryService)
        {
            _productDetailService = productDetailService;
            _productService = productService;
            _firebaseImageService = firebaseImageService;
            _colorService = colorService;
            _sizeService = sizeService;
            _materialsService = materialsService;
            _brandService = brandService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int productId = -1,
            [FromQuery] int colorId = -1,
            [FromQuery] int sizeId = -1,
            [FromQuery] int materialId = -1,
            [FromQuery] int brandId = -1,
            [FromQuery] int categoryId = -1,
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _productDetailService.GetList(
                    productId, colorId, sizeId, materialId, brandId, categoryId, keySearch, status, pagination);

                var listData = new BaseListDataResponse<ProductDetailResponse>
                {
                    List = result.Data.Select(p => new ProductDetailResponse { ProductDetail = p }).ToList(),
                    TotalRecord = result.TotalRecord
                };

                return OkWithData(listData);
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
                var productDetail = await _productDetailService.GetById(id);
                if (productDetail == null)
                    return BadRequestWithMessage("Product detail not found");

                return OkWithData(new ProductDetailResponse { ProductDetail = productDetail });
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
                var productDetail = await _productDetailService.GetById(id);
                if (productDetail == null)
                    return BadRequestWithMessage("Product detail not found");

                productDetail.Status = productDetail.Status == 1 ? 0 : 1;
                await _productDetailService.Update(productDetail);

                return OkWithData(new ProductDetailResponse { ProductDetail = productDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDProductDetailRequest request)
        {
            try
            {
                var product = await _productService.GetById(request.ProductId);
                if (product == null)
                    return BadRequestWithMessage("Product not found");

                var color = await _colorService.GetById(request.ColorId);
                if (color == null)
                    return BadRequestWithMessage("Color not found");

                var size = await _sizeService.GetById(request.SizeId);
                if (size == null)
                    return BadRequestWithMessage("Size not found");

                var material = await _materialsService.GetById(request.MaterialId);
                if (material == null)
                    return BadRequestWithMessage("Material not found");

                var brand = await _brandService.GetById(product.BrandId);
                if (brand == null)
                    return BadRequestWithMessage("Brand not found");

                var category = await _categoryService.GetById(product.CategoryId);
                if (category == null)
                    return BadRequestWithMessage("Category not found");

                var productDetail = new ProductDetail
                {
                    Name = request.Name,
                    ProductId = request.ProductId,
                    ColorId = color.Id,
                    Color = color.Name,
                    SizeId = size.Id,
                    Size = size.Name,
                    MaterialId = material.Id,
                    Material = material.Name,
                    Price = request.Price,
                    Stock = request.Stock,
                    Status = 1,
                    BrandId = brand.Id,
                    Brand = brand.Name,
                    CategoryId = category.Id,
                    Category = category.Name
                };

                // Check SKU exists
                var sku = $"S-{request.ProductId}-{request.ColorId}-{request.SizeId}-{request.MaterialId}";
                var existedBySku = await _productDetailService.GetBySku(sku);
                if (existedBySku != null)
                    return BadRequestWithMessage("Product detail with this SKU already exists");
                productDetail.Sku = sku;

                // Generate EAN-13 barcode
                var barcode = GenerateEAN13Barcode(request.ProductId, request.ColorId, request.SizeId, request.MaterialId);
                var existedByBarcode = await _productDetailService.GetByBarcode(barcode);
                if (existedByBarcode != null)
                    return BadRequestWithMessage($"Product detail with barcode {barcode} already exists");
                productDetail.Barcode = barcode;

                await _productDetailService.Create(productDetail);
                return OkWithData(new ProductDetailResponse { ProductDetail = productDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("create-multiple")]
        public async Task<IActionResult> CreateMultiple([FromBody] List<CRUDProductDetailRequest> requests)
        {
            try
            {
                var responses = new List<ProductDetailResponse>();
                var product = await _productService.GetById(requests[0].ProductId);
                if (product == null)
                    return BadRequestWithMessage("Product not found");

                var brand = await _brandService.GetById(product.BrandId);
                if (brand == null)
                    return BadRequestWithMessage("Brand not found");

                var category = await _categoryService.GetById(product.CategoryId);
                if (category == null)
                    return BadRequestWithMessage("Category not found");

                // Get all required entities in bulk
                var colorIds = requests.Select(r => r.ColorId).Distinct().ToList();
                var sizeIds = requests.Select(r => r.SizeId).Distinct().ToList();
                var materialIds = requests.Select(r => r.MaterialId).Distinct().ToList();

                var colors = await _colorService.GetByIds(colorIds);
                var sizes = await _sizeService.GetByIds(sizeIds);
                var materials = await _materialsService.GetByIds(materialIds);

                var colorMap = colors.ToDictionary(c => c.Id);
                var sizeMap = sizes.ToDictionary(s => s.Id);
                var materialMap = materials.ToDictionary(m => m.Id);

                foreach (var request in requests)
                {
                    if (!colorMap.TryGetValue(request.ColorId, out var color))
                        return BadRequestWithMessage("Color not found");

                    if (!sizeMap.TryGetValue(request.SizeId, out var size))
                        return BadRequestWithMessage("Size not found");

                    if (!materialMap.TryGetValue(request.MaterialId, out var material))
                        return BadRequestWithMessage("Material not found");

                    var productDetail = new ProductDetail
                    {
                        Name = request.Name,
                        ProductId = request.ProductId,
                        ColorId = color.Id,
                        Color = color.Name,
                        SizeId = size.Id,
                        Size = size.Name,
                        MaterialId = material.Id,
                        Material = material.Name,
                        Price = product.Price,
                        Stock = request.Stock,
                        Status = 1,
                        BrandId = brand.Id,
                        Brand = brand.Name,
                        CategoryId = category.Id,
                        Category = category.Name,
                        ImageUrl = product.ImageUrl
                    };

                    // Check SKU exists
                    var sku = $"S-{request.ProductId}-{request.ColorId}-{request.SizeId}-{request.MaterialId}";
                    var existedBySku = await _productDetailService.GetBySku(sku);
                    if (existedBySku != null)
                        return BadRequestWithMessage($"Product detail with SKU {sku} already exists");
                    productDetail.Sku = sku;

                    // Generate EAN-13 barcode
                    var barcode = GenerateEAN13Barcode(request.ProductId, request.ColorId, request.SizeId, request.MaterialId);
                    var existedByBarcode = await _productDetailService.GetByBarcode(barcode);
                    if (existedByBarcode != null)
                        return BadRequestWithMessage($"Product detail with barcode {barcode} already exists");
                    productDetail.Barcode = barcode;

                    await _productDetailService.Create(productDetail);
                    responses.Add(new ProductDetailResponse { ProductDetail = productDetail });
                }

                return OkWithData(responses);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDProductDetailRequest request)
        {
            try
            {
                var productDetail = await _productDetailService.GetById(id);
                if (productDetail == null)
                    return BadRequestWithMessage("Product detail not found");

                if (productDetail.Name != request.Name)
                {
                    var existingName = await _productDetailService.GetByName(request.Name);
                    if (existingName != null)
                        return BadRequestWithMessage("Product detail with this name already exists");
                }

                if (request.BrandId > 0)
                {
                    var brand = await _brandService.GetById(request.BrandId);
                    if (brand == null)
                        return BadRequestWithMessage("Brand not found");
                    productDetail.BrandId = brand.Id;
                    productDetail.Brand = brand.Name;
                }

                if (request.CategoryId > 0)
                {
                    var category = await _categoryService.GetById(request.CategoryId);
                    if (category == null)
                        return BadRequestWithMessage("Category not found");
                    productDetail.CategoryId = category.Id;
                    productDetail.Category = category.Name;
                }

                if (request.ColorId > 0)
                {
                    var color = await _colorService.GetById(request.ColorId);
                    if (color == null)
                        return BadRequestWithMessage("Color not found");
                    productDetail.ColorId = color.Id;
                    productDetail.Color = color.Name;
                }

                if (request.SizeId > 0)
                {
                    var size = await _sizeService.GetById(request.SizeId);
                    if (size == null)
                        return BadRequestWithMessage("Size not found");
                    productDetail.SizeId = size.Id;
                    productDetail.Size = size.Name;
                }

                if (request.MaterialId > 0)
                {
                    var material = await _materialsService.GetById(request.MaterialId);
                    if (material == null)
                        return BadRequestWithMessage("Material not found");
                    productDetail.MaterialId = material.Id;
                    productDetail.Material = material.Name;
                }

                productDetail.Name = request.Name;
                productDetail.Price = request.Price;
                productDetail.Stock = request.Stock;

                await _productDetailService.Update(productDetail);
                return OkWithData(new ProductDetailResponse { ProductDetail = productDetail });
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
                var productDetail = await _productDetailService.GetById(id);
                if (productDetail == null)
                    return BadRequestWithMessage("Product detail not found");

                var fileName = await _firebaseImageService.SaveAsync(file);
                var imageUrl = await _firebaseImageService.GetImageUrlAsync(fileName);

                productDetail.ImageUrl = imageUrl;
                await _productDetailService.Update(productDetail);

                return OkWithData(new ProductDetailResponse { ProductDetail = productDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/update-image")]
        public async Task<IActionResult> UpdateImageColorProduct(int id)
        {
            try
            {
                var productDetail = await _productDetailService.GetById(id);
                if (productDetail == null)
                    return BadRequestWithMessage("Product detail not found");

                var pagination = new Pagination(20, 0);
                var result = await _productDetailService.GetList(
                    productDetail.ProductId, productDetail.ColorId, -1, -1, -1, -1, "", 1, pagination);

                foreach (var pd in result.Data)
                {
                    pd.ImageUrl = productDetail.ImageUrl;
                    await _productDetailService.Update(pd);
                }

                return OkWithData(new ProductDetailResponse { ProductDetail = productDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            try
            {
                var productDetail = await _productDetailService.GetByBarcode(barcode);
                if (productDetail == null)
                    return BadRequestWithMessage($"Product detail with barcode {barcode} not found");

                return OkWithData(new ProductDetailResponse { ProductDetail = productDetail });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("barcode-image/{barcode}")]
        public async Task<IActionResult> GetBarcodeImage(string barcode)
        {
            try
            {
                var image = BarcodeUtil.GenerateEAN13BarcodeImage(barcode);
                return File(image, "image/png");
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        private string GenerateEAN13Barcode(int productId, int colorId, int sizeId, int materialId)
        {
            // EAN-13: 13 digits, e.g.: 893 + productId(5) + colorId(2) + sizeId(2) + materialId(1) + checksum(1)
            var prefix = "89";
            var body = $"{productId:D5}{colorId:D2}{sizeId:D2}{materialId:D1}";
            var partial = prefix + body; // 12 digits
            var checksum = CalcEAN13Checksum(partial);
            return partial + checksum;
        }

        private int CalcEAN13Checksum(string code)
        {
            var sum = 0;
            for (var i = 0; i < code.Length; i++)
            {
                var digit = code[i] - '0';
                sum += (i % 2 == 0) ? digit : digit * 3;
            }
            return (10 - (sum % 10)) % 10;
        }
    }
} 