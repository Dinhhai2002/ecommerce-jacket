using System.Threading.Tasks;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;

namespace webecommerce.Services
{
    public interface IGHNService
    {
        Task<GHNServiceResponse> GetAvailableServices(GHNServiceRequest request);
        Task<GHNFeeResponse> CalculateShippingFee(GHNFeeRequest request);
    }
} 