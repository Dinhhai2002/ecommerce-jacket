using Microsoft.AspNetCore.Mvc;
using webecommerce.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace webecommerce.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult OkWithData<T>(T data)
        {
            return Ok(new { Status = 200, Data = data });
        }

        protected IActionResult OkWithMessage(string message)
        {
            return Ok(new { Status = 200, Message = message });
        }

        protected IActionResult BadRequestWithMessage(string message)
        {
            return BadRequest(new { Status = 400, Message = message });
        }

        protected IActionResult NotFoundWithMessage(string message)
        {
            return NotFound(new { Status = 404, Message = message });
        }

        protected IActionResult ServerErrorWithMessage(string message)
        {
            return StatusCode(500, new { Status = 500, Message = message });
        }

        protected async Task<User> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return null;

            // TODO: Get user from database
            return null;
        }
    }
} 