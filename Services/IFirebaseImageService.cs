using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace webecommerce.Services
{
    public interface IFirebaseImageService
    {
        Task<string> SaveAsync(IFormFile file);
        Task<string> GetImageUrlAsync(string fileName);
        Task DeleteAsync(string fileName);
    }
} 