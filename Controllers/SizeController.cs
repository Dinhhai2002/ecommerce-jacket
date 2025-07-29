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
    public class SizeController : BaseController
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
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
                var result = await _sizeService.GetList(keySearch, status, pagination);

                var listData = new BaseListDataResponse<SizeResponse>
                {
                    List = result.Data.Select(s => new SizeResponse { Size = s }).ToList(),
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
                var size = await _sizeService.GetById(id);
                if (size == null)
                    return BadRequestWithMessage("Size not found");

                return OkWithData(new SizeResponse { Size = size });
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
                var size = await _sizeService.GetById(id);
                if (size == null)
                    return BadRequestWithMessage("Size not found");

                size.Status = size.Status == 1 ? 0 : 1;
                await _sizeService.Update(size);

                return OkWithData(new SizeResponse { Size = size });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDSizeRequest request)
        {
            try
            {
                var existingSize = await _sizeService.GetByName(request.Name);
                if (existingSize != null)
                    return BadRequestWithMessage("Size already exists");

                var size = new Size
                {
                    Name = request.Name,
                    Status = 1
                };

                await _sizeService.Create(size);
                return OkWithData(new SizeResponse { Size = size });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDSizeRequest request)
        {
            try
            {
                var size = await _sizeService.GetById(id);
                if (size == null)
                    return BadRequestWithMessage("Size not found");

                if (size.Name != request.Name)
                {
                    var existingSize = await _sizeService.GetByName(request.Name);
                    if (existingSize != null)
                        return BadRequestWithMessage("Size name already exists");
                }

                size.Name = request.Name;
                await _sizeService.Update(size);

                return OkWithData(new SizeResponse { Size = size });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 