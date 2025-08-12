using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Services;
using webecommerce.Common.Utils;

namespace webecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _userService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var response = await _userService.RegisterAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StoreProcedureListResult<UserResponse>>> GetList(
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _userService.GetListAsync(searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<UserResponse>
            {
                Items = result.Items.Select(u => (UserResponse)u).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("by-role/{role}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StoreProcedureListResult<UserResponse>>> GetListByRole(
            string role,
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var pagination = new Pagination { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _userService.GetListByRoleAsync(role, searchKey, status, pagination);

            return Ok(new StoreProcedureListResult<UserResponse>
            {
                Items = result.Items.Select(u => (UserResponse)u).ToList(),
                TotalRecords = result.TotalRecords,
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> GetById(int id)
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);

                // Only admin can view other users' profiles
                if (id != userId && !User.IsInRole("Admin"))
                    return Forbid();

                var user = await _userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> GetProfile()
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                var user = await _userService.GetByIdAsync(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                var user = await _userService.UpdateProfileAsync(userId, request);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                await _userService.ChangePasswordAsync(userId, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var result = await _userService.ForgotPasswordAsync(request);
            if (!result)
                return NotFound("Email not found");

            return Ok("Password reset instructions have been sent to your email");
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await _userService.ResetPasswordAsync(request);
            if (!result)
                return BadRequest("Invalid token or email");

            return Ok("Password has been reset successfully");
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var response = await _userService.RefreshTokenAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("revoke-token")]
        [Authorize]
        public async Task<ActionResult> RevokeToken()
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                await _userService.RevokeTokenAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateStatus(int id, [FromBody] UpdateUserStatusRequest request)
        {
            try
            {
                await _userService.UpdateStatusAsync(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateRole(int id, [FromBody] UpdateUserRoleRequest request)
        {
            try
            {
                await _userService.UpdateRoleAsync(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 