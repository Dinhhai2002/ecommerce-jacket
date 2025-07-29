using Microsoft.AspNetCore.Mvc;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;
using System.Collections.Generic;
using System;

namespace webecommerce.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GHNController : BaseController
    {
        private readonly IGHNService _ghnService;

        public GHNController(IGHNService ghnService)
        {
            _ghnService = ghnService;
        }

        [HttpPost("available-services")]
        public async Task<IActionResult> GetAvailableServices([FromBody] GHNServiceRequest request)
        {
            try
            {
                var response = await _ghnService.GetAvailableServices(request);
                return OkWithData(response.Data);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("calculate-fee")]
        public async Task<IActionResult> CalculateShippingFee([FromBody] GHNFeeRequest request)
        {
            try
            {
                var response = await _ghnService.CalculateShippingFee(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GHNFeeDetailResponse
                {
                    Code = 500,
                    Message = $"Error: {ex.Message}"
                });
            }
        }
    }
} 