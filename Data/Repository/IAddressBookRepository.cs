using System.Collections.Generic;

namespace webecommerce.Data.Repository
{
    public interface IAddressBookRepository : IGenericRepository<AddressBook>
    {
        List<AddressBook> FindByUserId(int userId);
        AddressBook FindDefaultByUserId(int userId);
        List<AddressBook> FindByStatus(int status);
        List<AddressBook> FindByUserIdAndStatus(int userId, int status);
    }
} 