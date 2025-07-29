namespace webecommerce.Models.Responses
{
    public class CartResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public CartResponse() {}
        public CartResponse(webecommerce.Data.Cart cart)
        {
            Id = cart.Id;
            UserId = cart.UserId;
            Status = cart.Status;
        }
    }
} 