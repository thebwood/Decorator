using Decorator.BusinessLayer.Services.Interfaces;
using Decorator.ClassLibrary.Dtos;
using Decorator.ClassLibrary.Models;

namespace Decorator.BusinessLayer.Decorators
{
    public class AddressServiceDecorator : IAddressService
    {
        private readonly IAddressService _inner;

        public AddressServiceDecorator(IAddressService inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public virtual async Task<AddressModel> GetAddressById(Guid id)
        {
            return await _inner.GetAddressById(id);
        }

        public virtual async Task<List<AddressModel>> GetAllAddresses()
        {
            return await _inner.GetAllAddresses();
        }

        public virtual async Task<List<AddressModel>> GetAddressesByFilter(GetAddressesRequestDTO requestDTO)
        {
            return await _inner.GetAddressesByFilter(requestDTO);
        }

        public virtual async Task<AddressModel> CreateAddress(AddressModel address)
        {
            return await _inner.CreateAddress(address);
        }

        public virtual async Task<AddressModel> UpdateAddress(AddressModel address)
        {
            return await _inner.UpdateAddress(address);
        }

        public virtual async Task<bool> DeleteAddress(Guid id)
        {
            return await _inner.DeleteAddress(id);
        }
    }
}
