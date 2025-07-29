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
    public class AddressBookController : BaseController
    {
        private readonly IAddressBookService _addressBookService;

        public AddressBookController(IAddressBookService addressBookService)
        {
            _addressBookService = addressBookService;
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
                var currentUser = await GetCurrentUser();
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _addressBookService.GetList(currentUser.Id, keySearch, status, pagination);

                var listData = new BaseListDataResponse<AddressBookResponse>
                {
                    List = result.Data.Select(a => new AddressBookResponse { AddressBook = a }).ToList(),
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
        public async Task<IActionResult> GetAllAdmin(
            [FromQuery] int userId = -1,
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            try
            {
                var pagination = new Pagination(limit, (page - 1) * limit);
                var result = await _addressBookService.GetList(userId, keySearch, status, pagination);

                var listData = new BaseListDataResponse<AddressBookResponse>
                {
                    List = result.Data.Select(a => new AddressBookResponse { AddressBook = a }).ToList(),
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
                var currentUser = await GetCurrentUser();
                var addressBook = await _addressBookService.GetById(id);

                if (addressBook == null)
                    return BadRequestWithMessage("Address not found");

                return OkWithData(new AddressBookResponse { AddressBook = addressBook });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CRUDAddressBookRequest request)
        {
            try
            {
                var currentUser = await GetCurrentUser();

                var addressBook = new AddressBook
                {
                    UserId = currentUser.Id,
                    FullName = request.FullName,
                    Phone = request.Phone,
                    WardId = request.WardId,
                    WardName = request.WardName,
                    DistrictId = request.DistrictId,
                    DistrictName = request.DistrictName,
                    CityId = request.CityId,
                    CityName = request.CityName,
                    FullAddress = request.FullAddress,
                    IsDefault = request.IsDefault,
                    Status = 1
                };

                await _addressBookService.Create(addressBook);

                if (request.IsDefault == 1)
                {
                    await _addressBookService.SetDefaultAddress(currentUser.Id, addressBook.Id);
                }

                return OkWithData(new AddressBookResponse { AddressBook = addressBook });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CRUDAddressBookRequest request)
        {
            try
            {
                var currentUser = await GetCurrentUser();
                var addressBook = await _addressBookService.GetById(id);

                if (addressBook == null || addressBook.UserId != currentUser.Id)
                    return BadRequestWithMessage("Address not found");

                addressBook.FullName = request.FullName;
                addressBook.Phone = request.Phone;
                addressBook.WardId = request.WardId;
                addressBook.WardName = request.WardName;
                addressBook.DistrictId = request.DistrictId;
                addressBook.DistrictName = request.DistrictName;
                addressBook.CityId = request.CityId;
                addressBook.CityName = request.CityName;
                addressBook.FullAddress = request.FullAddress;
                addressBook.IsDefault = request.IsDefault;

                await _addressBookService.Update(addressBook);

                if (request.IsDefault == 1)
                {
                    await _addressBookService.SetDefaultAddress(currentUser.Id, addressBook.Id);
                }

                return OkWithData(new AddressBookResponse { AddressBook = addressBook });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/change-status")]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            try
            {
                var currentUser = await GetCurrentUser();
                var addressBook = await _addressBookService.GetById(id);

                if (addressBook == null || addressBook.UserId != currentUser.Id)
                    return BadRequestWithMessage("Address not found");

                addressBook.Status = addressBook.Status == 1 ? 0 : 1;
                await _addressBookService.Update(addressBook);

                return OkWithData(new AddressBookResponse { AddressBook = addressBook });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("{id}/set-default")]
        public async Task<IActionResult> SetDefault(int id)
        {
            try
            {
                var currentUser = await GetCurrentUser();
                var addressBook = await _addressBookService.GetById(id);

                if (addressBook == null || addressBook.UserId != currentUser.Id)
                    return BadRequestWithMessage("Address not found");

                await _addressBookService.SetDefaultAddress(currentUser.Id, id);
                addressBook = await _addressBookService.GetById(id); // Refresh data

                return OkWithData(new AddressBookResponse { AddressBook = addressBook });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 