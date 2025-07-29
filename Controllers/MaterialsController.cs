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
    public class MaterialsController : BaseController
    {
        private readonly IMaterialsService _materialsService;

        public MaterialsController(IMaterialsService materialsService)
        {
            _materialsService = materialsService;
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
                var result = await _materialsService.GetList(keySearch, status, pagination);

                var listData = new BaseListDataResponse<MaterialsResponse>
                {
                    List = result.Data.Select(m => new MaterialsResponse { Materials = m }).ToList(),
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
                var materials = await _materialsService.GetById(id);
                if (materials == null)
                    return BadRequestWithMessage("Materials not found");

                return OkWithData(new MaterialsResponse { Materials = materials });
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
                var materials = await _materialsService.GetById(id);
                if (materials == null)
                    return BadRequestWithMessage("Materials not found");

                materials.Status = materials.Status == 1 ? 0 : 1;
                await _materialsService.Update(materials);

                return OkWithData(new MaterialsResponse { Materials = materials });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDMaterialsRequest request)
        {
            try
            {
                var existingMaterials = await _materialsService.GetByName(request.Name);
                if (existingMaterials != null)
                    return BadRequestWithMessage("Materials already exists");

                var materials = new Materials
                {
                    Name = request.Name,
                    Status = 1
                };

                await _materialsService.Create(materials);
                return OkWithData(new MaterialsResponse { Materials = materials });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDMaterialsRequest request)
        {
            try
            {
                var materials = await _materialsService.GetById(id);
                if (materials == null)
                    return BadRequestWithMessage("Materials not found");

                if (materials.Name != request.Name)
                {
                    var existingMaterials = await _materialsService.GetByName(request.Name);
                    if (existingMaterials != null)
                        return BadRequestWithMessage("Materials name already exists");
                }

                materials.Name = request.Name;
                await _materialsService.Update(materials);

                return OkWithData(new MaterialsResponse { Materials = materials });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 