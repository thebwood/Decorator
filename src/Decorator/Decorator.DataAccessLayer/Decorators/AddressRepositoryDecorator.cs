using Decorator.ClassLibrary.Dtos;
using Decorator.ClassLibrary.Models;
using Decorator.DataAccessLayer.Repositories.Interfaces;

namespace Decorator.DataAccessLayer.Decorators
{
    public class AddressRepositoryDecorator : IAddressRepository
    {
        private readonly IAddressRepository _inner;

        public AddressRepositoryDecorator(IAddressRepository inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public virtual async Task<List<AddressModel>> GetAllAddresses()
        {
            // Add custom behavior here
            return await _inner.GetAllAddresses();
        }

        public virtual async Task<List<AddressModel>> GetAddressesByFilter(GetAddressesRequestDTO requestDTO)
        {
            // Add custom behavior here
            return await _inner.GetAddressesByFilter(requestDTO);
        }

        public virtual async Task<AddressModel?> GetAddressById(Guid id)
        {
            // Add custom behavior here
            return await _inner.GetAddressById(id);
        }

        public virtual async Task<AddressModel> CreateAddress(AddressModel address)
        {
            // Add custom behavior here
            return await _inner.CreateAddress(address);
        }

        public virtual async Task<AddressModel> UpdateAddress(AddressModel address)
        {
            // Add custom behavior here
            return await _inner.UpdateAddress(address);
        }

        public virtual async Task<bool> DeleteAddress(Guid id)
        {
            // Add custom behavior here
            return await _inner.DeleteAddress(id);
        }
    }
}
