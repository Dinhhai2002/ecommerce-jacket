using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webecommerce.Data;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public AuthController(IConfiguration configuration, AppDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        // Mã hóa password (giả lập encodeBase64 như Java)
        string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(login.Password));

        // Kiểm tra thông tin đăng nhập (so sánh password đã mã hóa)
        var user = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.PasswordHash == encodedPassword);
        if (user == null)
        {
            return Unauthorized(new BaseResponse<string> { Status = 401, MessageError = "Invalid credentials" });
        }

        // Kiểm tra trạng thái tài khoản
        if (user.IsActive == 0)
        {
            return Unauthorized(new BaseResponse<string> { Status = 401, MessageError = "User is locked" });
        }

        // Kiểm tra cart, nếu chưa có thì tạo mới
        var cart = _context.Carts.FirstOrDefault(c => c.UserId == user.Id && c.Status == 1);
        if (cart == null)
        {
            var newCart = new webecommerce.Data.Cart
            {
                UserId = user.Id,
                Status = 1
            };
            _context.Carts.Add(newCart);
            _context.SaveChanges();
        }

        // Tạo JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new BaseResponse<JwtResponse>(new JwtResponse(tokenString)));
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        // Kiểm tra username/email/phone đã tồn tại chưa
        if (_context.Users.Any(u => u.Username == request.UserName))
        {
            return BadRequest(new BaseResponse<string> { Status = 400, MessageError = "Username already exists" });
        }
        if (_context.Users.Any(u => u.Email == request.Email))
        {
            return BadRequest(new BaseResponse<string> { Status = 400, MessageError = "Email already exists" });
        }
        if (_context.Users.Any(u => u.Phone == request.Phone))
        {
            return BadRequest(new BaseResponse<string> { Status = 400, MessageError = "Phone already exists" });
        }

        // Mã hóa password (giả lập encodeBase64 như Java)
        string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));

        var user = new webecommerce.Data.User
        {
            Username = request.UserName,
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            PasswordHash = encodedPassword,
            Gender = request.Gender,
            Birthday = request.Birthday,
            WardId = request.WardId,
            DistrictId = request.DistrictId,
            CityId = request.CityId,
            FullAddress = request.FullAddress,
            Role = request.Role,
            IsActive = 1,
            IsGoogle = 0,
            IsLogin = 0,
            IsConfirmOtp = 0
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(new BaseResponse<UserResponse>(new UserResponse(user)));
    }
}