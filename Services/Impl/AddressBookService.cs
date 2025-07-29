using System.Collections.Generic;
using webecommerce.Data;
using webecommerce.Data.Repository;

namespace webecommerce.Services.Impl
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBookRepository _addressBookRepository;

        public AddressBookService(IAddressBookRepository addressBookRepository)
        {
            _addressBookRepository = addressBookRepository;
        }

        public void Create(AddressBook addressBook)
        {
            _addressBookRepository.Create(addressBook);
        }

        public AddressBook FindOne(int id)
        {
            return _addressBookRepository.FindOne(id);
        }

        public void Update(AddressBook addressBook)
        {
            _addressBookRepository.Update(addressBook);
        }

        public List<AddressBook> GetAll()
        {
            return _addressBookRepository.GetAll();
        }

        public List<AddressBook> FindByUserId(int userId)
        {
            return _addressBookRepository.FindByUserId(userId);
        }

        public AddressBook FindDefaultByUserId(int userId)
        {
            return _addressBookRepository.FindDefaultByUserId(userId);
        }

        public List<AddressBook> FindByStatus(int status)
        {
            return _addressBookRepository.FindByStatus(status);
        }

        public List<AddressBook> FindByUserIdAndStatus(int userId, int status)
        {
            return _addressBookRepository.FindByUserIdAndStatus(userId, status);
        }

        public List<AddressBook> FindAllActive()
        {
            return _addressBookRepository.FindAllActive();
        }
    }
} 