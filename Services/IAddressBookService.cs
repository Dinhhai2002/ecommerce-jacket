using System.Collections.Generic;
using webecommerce.Data;

namespace webecommerce.Services
{
    public interface IAddressBookService
    {
        void Create(AddressBook addressBook);
        AddressBook FindOne(int id);
        void Update(AddressBook addressBook);
        List<AddressBook> GetAll();
        List<AddressBook> FindByUserId(int userId);
        AddressBook FindDefaultByUserId(int userId);
        List<AddressBook> FindByStatus(int status);
        List<AddressBook> FindByUserIdAndStatus(int userId, int status);
        List<AddressBook> FindAllActive();
    }
} 