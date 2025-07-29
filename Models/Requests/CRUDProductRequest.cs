namespace webecommerce.Models.Requests
{
    public class CRUDProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
} 