using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class CategoryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("parent_id")]
        public int ParentId { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        public CategoryResponse() {}
        public CategoryResponse(webecommerce.Data.Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentId = category.ParentId;
            ImageUrl = category.ImageUrl;
            Status = category.Status;
        }
    }
} 