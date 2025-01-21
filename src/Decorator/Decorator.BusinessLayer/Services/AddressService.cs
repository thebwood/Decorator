﻿using Decorator.BusinessLayer.Services.Interfaces;
using Decorator.ClassLibrary.Dtos;
using Decorator.ClassLibrary.Models;
using Decorator.DataAccessLayer.Repositories.Interfaces;

namespace Decorator.BusinessLayer.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<AddressModel> CreateAddress(AddressModel address)
        {
            return await _addressRepository.CreateAddress(address);
        }

        public async Task<bool> DeleteAddress(Guid id)
        {
            return await _addressRepository.DeleteAddress(id);
        }

        public async Task<AddressModel> GetAddressById(Guid id)
        {
            return await _addressRepository.GetAddressById(id);
        }

        public async Task<List<AddressModel>> GetAddressesByFilter(GetAddressesRequestDTO requestDTO)
        {
            return await _addressRepository.GetAddressesByFilter(requestDTO);
        }

        public async Task<List<AddressModel>> GetAllAddresses()
        {
            return await _addressRepository.GetAllAddresses();
        }

        public async Task<AddressModel> UpdateAddress(AddressModel address)
        {
            return await _addressRepository.UpdateAddress(address);
        }
    }
}
