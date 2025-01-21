using Decorator.ClassLibrary.Dtos;
using Decorator.ClassLibrary.Models;

namespace Decorator.DataAccessLayer.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<AddressModel>> GetAllAddresses();
        Task<List<AddressModel>> GetAddressesByFilter(GetAddressesRequestDTO requestDTO);
        Task<AddressModel?> GetAddressById(Guid id);
        Task<AddressModel> CreateAddress(AddressModel address);
        Task<AddressModel> UpdateAddress(AddressModel address);
        Task<bool> DeleteAddress(Guid id);
    }
}
