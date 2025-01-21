using Decorator.BusinessLayer.Services.Interfaces;
using Decorator.ClassLibrary.Dtos;
using Decorator.ClassLibrary.Models;
using Microsoft.Extensions.Logging;

namespace Decorator.BusinessLayer.Decorators
{
    public class LoggingAddressServiceDecorator : AddressServiceDecorator
    {
        private readonly ILogger<LoggingAddressServiceDecorator> _logger;

        public LoggingAddressServiceDecorator(IAddressService inner, ILogger<LoggingAddressServiceDecorator> logger)
            : base(inner)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<AddressModel> GetAddressById(Guid id)
        {
            _logger.LogInformation("BLL - Calling GetAddressById with ID: {Id}", id);
            var result = await base.GetAddressById(id);
            _logger.LogInformation("BLL - GetAddressById returned: {@AddressModel}", result);
            return result;
        }

        public override async Task<List<AddressModel>> GetAllAddresses()
        {
            _logger.LogInformation("BLL - Calling GetAllAddresses.");
            var result = await base.GetAllAddresses();
            _logger.LogInformation("BLL - GetAllAddresses returned {Count} addresses.", result.Count);
            return result;
        }

        public override async Task<List<AddressModel>> GetAddressesByFilter(GetAddressesRequestDTO requestDTO)
        {
            _logger.LogInformation("BLL - Calling GetAddressesByFilter with filter: {@RequestDTO}", requestDTO);
            var result = await base.GetAddressesByFilter(requestDTO);
            _logger.LogInformation("BLL - GetAddressesByFilter returned {Count} addresses.", result.Count);
            return result;
        }

        public override async Task<AddressModel> CreateAddress(AddressModel address)
        {
            _logger.LogInformation("BLL - Calling CreateAddress with details: {@AddressModel}", address);
            var result = await base.CreateAddress(address);
            _logger.LogInformation("BLL - CreateAddress created: {@AddressModel}", result);
            return result;
        }

        public override async Task<AddressModel> UpdateAddress(AddressModel address)
        {
            _logger.LogInformation("BLL - Calling UpdateAddress with details: {@AddressModel}", address);
            var result = await base.UpdateAddress(address);
            _logger.LogInformation("BLL - UpdateAddress updated to: {@AddressModel}", result);
            return result;
        }

        public override async Task<bool> DeleteAddress(Guid id)
        {
            _logger.LogInformation("BLL - Calling DeleteAddress with ID: {Id}", id);
            var result = await base.DeleteAddress(id);
            _logger.LogInformation("BLL - DeleteAddress result: {Result}", result);
            return result;
        }
    }
}
