namespace webecommerce.Models.Responses
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public JwtResponse() {}
        public JwtResponse(string token)
        {
            Token = token;
        }
    }
} 