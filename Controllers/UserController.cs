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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IFirebaseImageService _firebaseImageService;
        private readonly IImageService _imageService;
        private readonly ICartService _cartService;

        public UserController(
            IUserService userService,
            IFirebaseImageService firebaseImageService,
            IImageService imageService,
            ICartService cartService)
        {
            _userService = userService;
            _firebaseImageService = firebaseImageService;
            _imageService = imageService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string keySearch = "",
            [FromQuery] int status = -1,
            [FromQuery] int role = -1,
            [FromQuery] int page = 0,
            [FromQuery] int limit = 20)
        {
            try
            {
                var pagination = new Pagination(limit, page * limit);
                var result = await _userService.GetList(keySearch, status, role, pagination);

                var listData = new BaseListDataResponse<UserResponse>
                {
                    List = result.Data.Select(u => new UserResponse { User = u }).ToList(),
                    TotalRecord = result.TotalRecord
                };

                return OkWithData(listData);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] CRUDUserRequest request)
        {
            try
            {
                var user = await GetCurrentUser();

                if (user.Email != request.Email)
                {
                    var existingUserByEmail = await _userService.FindByEmail(request.Email, 0);
                    if (existingUserByEmail != null)
                        return BadRequestWithMessage("Email already exists");
                }

                if (user.Phone != request.Phone)
                {
                    var existingUserByPhone = await _userService.FindByPhone(request.Phone);
                    if (existingUserByPhone != null)
                        return BadRequestWithMessage("Phone number already exists");
                }

                user.FullName = request.FullName;
                user.Email = request.Email;
                user.Phone = request.Phone;
                user.FullAddress = request.FullAddress;

                await _userService.Update(user);
                return OkWithData(new UserResponse { User = user });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpGet("detail")]
        public async Task<IActionResult> GetDetail()
        {
            try
            {
                var user = await GetCurrentUser();
                var pagination = new Pagination(20, 0);
                var cartResult = await _cartService.GetList(user.Id, "", 1, pagination);
                var cart = cartResult.Data.FirstOrDefault();

                var response = new UserResponse 
                { 
                    User = user,
                    CartId = cart?.Id ?? 0
                };

                return OkWithData(response);
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var user = await GetCurrentUser();
                var currentPassword = Utils.DecodeBase64(user.Password);

                if (request.OldPassword != currentPassword)
                    return BadRequestWithMessage("Current password is incorrect");

                if (request.NewPassword != request.ConfirmPassword)
                    return BadRequestWithMessage("New password and confirm password do not match");

                user.Password = Utils.EncodeBase64(request.NewPassword);
                await _userService.Update(user);

                return OkWithData(new UserResponse { User = user });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }

        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            try
            {
                var user = await GetCurrentUser();

                var fileName = await _firebaseImageService.SaveAsync(file);
                var imageUrl = await _firebaseImageService.GetImageUrlAsync(fileName);

                user.AvatarUrl = imageUrl;
                await _userService.Update(user);

                return OkWithData(imageUrl);
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
                var currentUser = await GetCurrentUser();
                var user = await _userService.GetById(id);

                if (user == null)
                    return BadRequestWithMessage("User not found");

                if (user.Id == currentUser.Id)
                    return BadRequestWithMessage("Cannot change your own status");

                user.IsActive = user.IsActive == 1 ? 0 : 1;
                await _userService.Update(user);

                return OkWithData(new UserResponse { User = user });
            }
            catch (Exception ex)
            {
                return ServerErrorWithMessage(ex.Message);
            }
        }
    }
} 