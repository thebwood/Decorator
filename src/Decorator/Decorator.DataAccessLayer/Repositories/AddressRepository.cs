﻿using Decorator.ClassLibrary.Dtos;
using Decorator.ClassLibrary.Models;
using Decorator.DataAccessLayer.Data;
using Decorator.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Decorator.DataAccessLayer.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressDbContext _addressDbContext;
        public AddressRepository(AddressDbContext addressDbContext)
        {
            _addressDbContext = addressDbContext;
        }

        public async Task<AddressModel> CreateAddress(AddressModel address)
        {
            _addressDbContext.Addresses.Add(address);
            await _addressDbContext.SaveChangesAsync();
            return address;
        }

        public async Task<bool> DeleteAddress(Guid id)
        {
            _addressDbContext.Addresses.Remove(new AddressModel(id));
            await _addressDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<AddressModel?> GetAddressById(Guid id)
        {
            AddressModel? address = await _addressDbContext.Addresses.SingleOrDefaultAsync(x => x.Id == id);
            return address;
        }

        public async Task<List<AddressModel>> GetAddressesByFilter(GetAddressesRequestDTO requestDTO)
        {
            var skip = requestDTO.PageNumber * requestDTO.PageSize;
            var take = requestDTO.PageSize;

            List<AddressModel> addresses = await _addressDbContext.Addresses
                                                //.Where(a => a.StreetAddress.Contains(requestDTO.SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                //    a.City.Contains(requestDTO.SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                //    a.State.Contains(requestDTO.SearchText, StringComparison.OrdinalIgnoreCase))
                                                .OrderBy(a => a.Id)
                                                .Skip(skip)
                                                .Take(take)
                                                .ToListAsync();

            return addresses;
        }

        public async Task<List<AddressModel>> GetAllAddresses()
        {
            return await _addressDbContext.Addresses
                                                .Take(1000)
                                                .ToListAsync();
        }

        public async Task<AddressModel> UpdateAddress(AddressModel address)
        {
            _addressDbContext.Addresses.Update(address);
            await _addressDbContext.SaveChangesAsync();
            return address;
        }
    }
}
