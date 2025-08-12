using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Common.Utils;

namespace webecommerce.Services
{
    public interface IAddressBookService
    {
        Task<AddressBookResponse> GetByIdAsync(int id);
        Task<StoreProcedureListResult<AddressBook>> GetListByUserAsync(int userId, string searchKey = "", int status = 1, Pagination pagination = null);
        Task<AddressBookResponse> GetDefaultAddressAsync(int userId);
        Task<AddressBookResponse> CreateAsync(int userId, CreateAddressBookRequest request);
        Task<AddressBookResponse> UpdateAsync(int userId, int addressId, UpdateAddressBookRequest request);
        Task<bool> DeleteAsync(int userId, int addressId);
        Task<bool> SetDefaultAddressAsync(int userId, SetDefaultAddressRequest request);
        Task<IEnumerable<AddressBookResponse>> GetByUserIdAsync(int userId);
    }
} 