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
    public class ReturnRequestController : BaseController
    {
        private readonly IReturnRequestService _returnRequestService;
        private readonly IReturnRequestDetailService _returnRequestDetailService;
        private readonly IFirebaseImageService _firebaseImageService;

        public ReturnRequestController(
            IReturnRequestService returnRequestService,
            IReturnRequestDetailService returnRequestDetailService,
            IFirebaseImageService firebaseImageService)
        {
            _returnRequestService = returnRequestService;
            _returnRequestDetailService = returnRequestDetailService;
            _firebaseImageService = firebaseImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int userId = -1,
            [FromQuery] string keySearch = "",
            [FromQuery] string status = "",
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _returnRequestService.GetList(userId, keySearch, status, pagination);

                var responses = await Task.WhenAll(result.Data.Select(async request =>
                {
                    var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(request.Id);
                    return new ReturnRequestResponse { ReturnRequest = request, Details = details };
                }));

                var listData = new BaseListDataResponse<ReturnRequestResponse>
                {
                    List = responses.ToList(),
                    TotalRecord = result.TotalRecord
                };

                return OkWithData(listData);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReturnRequestRequest request)
        {
            try
            {
                var returnRequest = await _returnRequestService.Create(request);
                var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(returnRequest.Id);

                var response = new ReturnRequestResponse
                {
                    ReturnRequest = returnRequest,
                    Details = details
                };

                return OkWithData(response);
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
                var returnRequest = await _returnRequestService.GetById(id);
                if (returnRequest == null)
                    return BadRequestWithMessage("Return request not found");

                var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(returnRequest.Id);
                var response = new ReturnRequestResponse
                {
                    ReturnRequest = returnRequest,
                    Details = details
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var returnRequests = await _returnRequestService.GetByUserId(userId);
                var responses = await Task.WhenAll(returnRequests.Select(async request =>
                {
                    var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(request.Id);
                    return new ReturnRequestResponse { ReturnRequest = request, Details = details };
                }));

                return OkWithData(responses);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            try
            {
                var returnRequests = await _returnRequestService.GetByOrderId(orderId);
                var responses = await Task.WhenAll(returnRequests.Select(async request =>
                {
                    var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(request.Id);
                    return new ReturnRequestResponse { ReturnRequest = request, Details = details };
                }));

                return OkWithData(responses);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAllAdmin([FromQuery] string status = "")
        {
            try
            {
                var returnRequests = string.IsNullOrEmpty(status)
                    ? await _returnRequestService.GetAll()
                    : await _returnRequestService.GetByStatus(status);

                var responses = await Task.WhenAll(returnRequests.Select(async request =>
                {
                    var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(request.Id);
                    return new ReturnRequestResponse { ReturnRequest = request, Details = details };
                }));

                return OkWithData(responses);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(int id, [FromBody] ApproveReturnRequestRequest request)
        {
            try
            {
                var returnRequest = await _returnRequestService.Approve(id, request);
                var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(returnRequest.Id);

                var response = new ReturnRequestResponse
                {
                    ReturnRequest = returnRequest,
                    Details = details
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(int id, [FromBody] RejectReturnRequestRequest request)
        {
            try
            {
                var returnRequest = await _returnRequestService.Reject(id, request);
                var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(returnRequest.Id);

                var response = new ReturnRequestResponse
                {
                    ReturnRequest = returnRequest,
                    Details = details
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/process")]
        public async Task<IActionResult> Process(int id)
        {
            try
            {
                var returnRequest = await _returnRequestService.Process(id);
                var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(returnRequest.Id);

                var response = new ReturnRequestResponse
                {
                    ReturnRequest = returnRequest,
                    Details = details
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                var returnRequest = await _returnRequestService.Complete(id);
                var details = await _returnRequestDetailService.GetDetailsByReturnRequestId(returnRequest.Id);

                var response = new ReturnRequestResponse
                {
                    ReturnRequest = returnRequest,
                    Details = details
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            try
            {
                await _returnRequestService.Cancel(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _returnRequestService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            try
            {
                var fileName = await _firebaseImageService.SaveAsync(file);
                var imageUrl = await _firebaseImageService.GetImageUrlAsync(fileName);

                return OkWithData(imageUrl);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 