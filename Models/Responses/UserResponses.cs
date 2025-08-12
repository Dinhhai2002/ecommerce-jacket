using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class UserResponse : BaseResponse
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("lastLogin")]
        public DateTime? LastLogin { get; set; }

        [JsonProperty("addressBooks")]
        public virtual ICollection<AddressBookResponse> AddressBooks { get; set; }

        public static implicit operator UserResponse(User user)
        {
            if (user == null) return null;

            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Phone = user.Phone,
                Avatar = user.Avatar,
                Role = user.Role,
                LastLogin = user.LastLogin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Status = user.Status,
                AddressBooks = user.AddressBooks?.Select(a => (AddressBookResponse)a).ToList()
            };
        }
    }

    public class LoginResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("tokenType")]
        public string TokenType { get; set; } = "Bearer";

        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonProperty("user")]
        public UserResponse User { get; set; }
    }

    public class RefreshTokenResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("tokenType")]
        public string TokenType { get; set; } = "Bearer";

        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }
    }
} 