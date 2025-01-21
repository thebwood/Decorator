using Decorator.BusinessLayer.Services.Interfaces;
using Decorator.ClassLibrary.Models;

namespace Decorator.BusinessLayer.Decorators
{
    public class ValidationAddressServiceDecorator : AddressServiceDecorator
    {
        public ValidationAddressServiceDecorator(IAddressService inner) : base(inner) { }

        public override async Task<AddressModel> CreateAddress(AddressModel address)
        {
            if (address == null)
            {
                throw new ArgumentException("Address cannot be null");
            }

            Console.WriteLine("Validation passed for creating address.");
            return await base.CreateAddress(address);
        }

        public override async Task<bool> DeleteAddress(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID cannot be an empty GUID.");
            }

            Console.WriteLine("Validation passed for deleting address.");
            return await base.DeleteAddress(id);
        }
    }
}
