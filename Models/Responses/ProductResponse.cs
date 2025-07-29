namespace webecommerce.Models.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public ProductResponse() {}
        public ProductResponse(webecommerce.Data.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            BrandId = product.BrandId;
            CategoryId = product.CategoryId;
            Description = product.Description;
            Status = product.Status;
        }
    }
} 