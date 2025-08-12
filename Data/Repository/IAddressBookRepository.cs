using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Common.Utils;

namespace webecommerce.Data.Repository
{
    public interface IAddressBookRepository : IGenericRepository<AddressBook>
    {
        Task<StoreProcedureListResult<AddressBook>> GetListByUserAsync(int userId, string searchKey = "", int status = 1, Pagination pagination = null);
        Task<AddressBook> GetDefaultAddressAsync(int userId);
        Task<bool> SetDefaultAddressAsync(int userId, int addressId);
        Task<bool> UnsetDefaultAddressAsync(int userId);
        Task<bool> CheckAddressExistsAsync(int userId, string fullAddress);
        Task<bool> CheckUserOwnsAddressAsync(int userId, int addressId);
        Task<IEnumerable<AddressBook>> GetByUserIdAsync(int userId);
    }
} 