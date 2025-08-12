using Newtonsoft.Json;
using System;
namespace webecommerce.Models.Responses
{
    public class JwtResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        public JwtResponse() {}
        public JwtResponse(string token)
        {
            Token = token;
        }
    }
} 