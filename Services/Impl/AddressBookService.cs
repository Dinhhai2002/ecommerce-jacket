using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webecommerce.Models;
using webecommerce.Models.Requests;
using webecommerce.Models.Responses;
using webecommerce.Data.Repository;
using webecommerce.Common.Utils;

namespace webecommerce.Services.Impl
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBookRepository _addressBookRepository;

        public AddressBookService(
            IAddressBookRepository addressBookRepository)
        {
            _addressBookRepository = addressBookRepository;
        }

        public async Task<AddressBookResponse> GetByIdAsync(int id)
        {
            var address = await _addressBookRepository.GetByIdAsync(id);
            if (address == null)
                throw new Exception("Address not found");

            return address;
        }

        public async Task<StoreProcedureListResult<AddressBook>> GetListByUserAsync(
            int userId,
            string searchKey = "",
            int status = 1,
            Pagination pagination = null)
        {
            return await _addressBookRepository.GetListByUserAsync(userId, searchKey, status, pagination);
        }

        public async Task<AddressBookResponse> GetDefaultAddressAsync(int userId)
        {
            var address = await _addressBookRepository.GetDefaultAddressAsync(userId);
            return address;
        }

        public async Task<AddressBookResponse> CreateAsync(int userId, CreateAddressBookRequest request)
        {
           

            var address = new AddressBook
            {
                UserId = userId,
                FullName = request.FullName,
                Phone = request.Phone,
                CityId = request.CityId,
                CityName = request.CityName,
                DistrictId = request.DistrictId,
                DistrictName = request.DistrictName,
                WardId = request.WardId,
                WardName = request.WardName,
                FullAddress = request.FullAddress,
                IsDefault = request.IsDefault,
                Status = 1
            };

            if (address.IsDefault)
                await _addressBookRepository.UnsetDefaultAddressAsync(userId);

            address = await _addressBookRepository.AddAsync(address);
            return address;
        }

        public async Task<AddressBookResponse> UpdateAsync(int userId, int addressId, UpdateAddressBookRequest request)
        {
            var address = await _addressBookRepository.GetByIdAsync(addressId);
            if (address == null)
                throw new Exception("Address not found");

            if (!await _addressBookRepository.CheckUserOwnsAddressAsync(userId, addressId))
                throw new Exception("You don't have permission to update this address");

            if (address.FullAddress != request.FullAddress && 
                await _addressBookRepository.CheckAddressExistsAsync(userId, request.FullAddress))
                throw new Exception("Address already exists");

            address.FullName = request.FullName;
            address.Phone = request.Phone;
            address.CityId = request.CityId;
            address.CityName = request.CityName;
            address.DistrictId = request.DistrictId;
            address.DistrictName = request.DistrictName;
            address.WardId = request.WardId;
            address.WardName = request.WardName;
            address.FullAddress = request.FullAddress;
            address.Status = request.Status;

            if (request.IsDefault && !address.IsDefault)
            {
                await _addressBookRepository.UnsetDefaultAddressAsync(userId);
                address.IsDefault = true;
            }
            else if (!request.IsDefault && address.IsDefault)
            {
                var defaultAddressCount = await _addressBookRepository.GetListByUserAsync(userId, "", 1, new Pagination { PageSize = 1 });
                if (defaultAddressCount.TotalRecords > 1)
                    address.IsDefault = false;
            }

            address = await _addressBookRepository.UpdateAsync(address);
            return address;
        }

        public async Task<bool> DeleteAsync(int userId, int addressId)
        {
            var address = await _addressBookRepository.GetByIdAsync(addressId);
            if (address == null)
                return false;

            if (!await _addressBookRepository.CheckUserOwnsAddressAsync(userId, addressId))
                throw new Exception("You don't have permission to delete this address");

            if (address.IsDefault)
            {
                var defaultAddressCount = await _addressBookRepository.GetListByUserAsync(userId, "", 1, new Pagination { PageSize = 1 });
                if (defaultAddressCount.TotalRecords <= 1)
                    throw new Exception("Cannot delete the only default address");
            }

            return await _addressBookRepository.DeleteAsync(addressId);
        }

        public async Task<bool> SetDefaultAddressAsync(int userId, SetDefaultAddressRequest request)
        {
            if (!await _addressBookRepository.CheckUserOwnsAddressAsync(userId, request.AddressId))
                throw new Exception("You don't have permission to update this address");

            return await _addressBookRepository.SetDefaultAddressAsync(userId, request.AddressId);
        }

        public async Task<IEnumerable<AddressBookResponse>> GetByUserIdAsync(int userId)
        {
            var addresses = await _addressBookRepository.GetByUserIdAsync(userId);
            return addresses.Select(a => (AddressBookResponse)a);
        }
    }
} 