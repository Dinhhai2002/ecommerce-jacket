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
    public class ColorController : BaseController
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
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
                var result = await _colorService.GetList(keySearch, status, pagination);

                var listData = new BaseListDataResponse<ColorResponse>
                {
                    List = result.Data.Select(c => new ColorResponse { Color = c }).ToList(),
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
                var color = await _colorService.GetById(id);
                if (color == null)
                    return BadRequestWithMessage("Color not found");

                return OkWithData(new ColorResponse { Color = color });
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
                var color = await _colorService.GetById(id);
                if (color == null)
                    return BadRequestWithMessage("Color not found");

                color.Status = color.Status == 1 ? 0 : 1;
                await _colorService.Update(color);

                return OkWithData(new ColorResponse { Color = color });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDColorRequest request)
        {
            try
            {
                var existingColor = await _colorService.GetByName(request.Name);
                if (existingColor != null)
                    return BadRequestWithMessage("Color already exists");

                var color = new Color
                {
                    Name = request.Name,
                    Status = 1
                };

                await _colorService.Create(color);
                return OkWithData(new ColorResponse { Color = color });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDColorRequest request)
        {
            try
            {
                var color = await _colorService.GetById(id);
                if (color == null)
                    return BadRequestWithMessage("Color not found");

                if (color.Name != request.Name)
                {
                    var existingColor = await _colorService.GetByName(request.Name);
                    if (existingColor != null)
                        return BadRequestWithMessage("Color name already exists");
                }

                color.Name = request.Name;
                await _colorService.Update(color);

                return OkWithData(new ColorResponse { Color = color });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 