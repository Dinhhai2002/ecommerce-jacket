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

namespace webecommerce.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BannerController : BaseController
    {
        private readonly IBannerService _bannerService;
        private readonly IFirebaseImageService _firebaseImageService;

        public BannerController(
            IBannerService bannerService,
            IFirebaseImageService firebaseImageService)
        {
            _bannerService = bannerService;
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
                var result = await _bannerService.GetList(keySearch, status, pagination);

                var listData = new BaseListDataResponse<BannerResponse>
                {
                    List = result.Data.Select(b => new BannerResponse { Banner = b }).ToList(),
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
                var banner = await _bannerService.GetById(id);
                if (banner == null)
                    return BadRequestWithMessage("Banner not found");

                return OkWithData(new BannerResponse { Banner = banner });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file)
        {
            try
            {
                var fileName = await _firebaseImageService.SaveAsync(file);
                var imageUrl = await _firebaseImageService.GetImageUrlAsync(fileName);

                var banner = new Banner
                {
                    Url = imageUrl,
                    Status = 1,
                    IsDeleted = 0
                };

                await _bannerService.Create(banner);
                return OkWithData(new BannerResponse { Banner = banner });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDBannerRequest request)
        {
            try
            {
                var banner = await _bannerService.GetById(id);
                if (banner == null)
                    return BadRequestWithMessage("Banner not found");

                banner.Url = request.Url;
                await _bannerService.Update(banner);

                return OkWithData(new BannerResponse { Banner = banner });
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
                var banner = await _bannerService.GetById(id);
                if (banner == null)
                    return BadRequestWithMessage("Banner not found");

                banner.Status = banner.Status == 1 ? 0 : 1;
                await _bannerService.Update(banner);

                return OkWithData(new BannerResponse { Banner = banner });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 