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
    [Authorize]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookService _addressBookService;

        public AddressBookController(IAddressBookService addressBookService)
        {
            _addressBookService = addressBookService;
        }

        [HttpGet]
        public async Task<ActionResult<StoreProcedureListResult<AddressBookResponse>>> GetList(
            [FromQuery] string searchKey = "",
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                var result = await _addressBookService.GetListByUserAsync(userId, searchKey, status, new Pagination { PageNumber = pageNumber, PageSize = pageSize });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressBookResponse>> GetById(int id)
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);

                var address = await _addressBookService.GetByIdAsync(id);
                if (address.UserId != userId && !User.IsInRole("Admin"))
                    return Forbid();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("default")]
        public async Task<ActionResult<AddressBookResponse>> GetDefaultAddress()
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                var address = await _addressBookService.GetDefaultAddressAsync(userId);
                if (address == null)
                    return NotFound();

                return Ok(address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AddressBookResponse>> Create([FromBody] CreateAddressBookRequest request)
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                var address = await _addressBookService.CreateAsync(userId, request);
                return CreatedAtAction(nameof(GetById), new { id = address.Id }, address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AddressBookResponse>> Update(int id, [FromBody] UpdateAddressBookRequest request)
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                var address = await _addressBookService.UpdateAsync(userId, id, request);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                var result = await _addressBookService.DeleteAsync(userId, id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("set-default")]
        public async Task<ActionResult> SetDefaultAddress([FromBody] SetDefaultAddressRequest request)
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                var result = await _addressBookService.SetDefaultAddressAsync(userId, request);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<AddressBookResponse>>> GetAllAddresses()
        {
            try
            {
                // Get current user ID from claims
                var userId = int.Parse(User.FindFirst("sub")?.Value);
                var addresses = await _addressBookService.GetByUserIdAsync(userId);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
} 