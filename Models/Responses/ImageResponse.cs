using Newtonsoft.Json;

namespace webecommerce.Models.Responses
{
    public class ImageResponse : BaseResponse
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileSize")]
        public long FileSize { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("entityType")]
        public string EntityType { get; set; }

        [JsonProperty("entityId")]
        public int EntityId { get; set; }

        public static implicit operator ImageResponse(Image image)
        {
            if (image == null) return null;

            return new ImageResponse
            {
                Id = image.Id,
                Url = image.Url,
                FileName = image.FileName,
                FileSize = image.FileSize,
                MimeType = image.MimeType,
                EntityType = image.EntityType,
                EntityId = image.EntityId,
                CreatedAt = image.CreatedAt,
                UpdatedAt = image.UpdatedAt,
                Status = image.Status
            };
        }
    }
} 