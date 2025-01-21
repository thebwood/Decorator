﻿using Decorator.ClassLibrary.Dtos;
using Decorator.ClassLibrary.Models;
using Decorator.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator.DataAccessLayer.Decorators
{
    public class LoggingAddressRepositoryDecorator : AddressRepositoryDecorator
    {
        private readonly ILogger<LoggingAddressRepositoryDecorator> _logger;

        public LoggingAddressRepositoryDecorator(IAddressRepository inner, ILogger<LoggingAddressRepositoryDecorator> logger)
            : base(inner)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<AddressModel> GetAddressById(Guid id)
        {
            _logger.LogInformation("Calling GetAddressById with ID: {Id}", id);
            var result = await base.GetAddressById(id);
            _logger.LogInformation("GetAddressById returned: {@AddressModel}", result);
            return result;
        }

        public override async Task<List<AddressModel>> GetAllAddresses()
        {
            _logger.LogInformation("Calling GetAllAddresses.");
            var result = await base.GetAllAddresses();
            _logger.LogInformation("GetAllAddresses returned {Count} addresses.", result.Count);
            return result;
        }

        public override async Task<List<AddressModel>> GetAddressesByFilter(GetAddressesRequestDTO requestDTO)
        {
            _logger.LogInformation("Calling GetAddressesByFilter with filter: {@RequestDTO}", requestDTO);
            var result = await base.GetAddressesByFilter(requestDTO);
            _logger.LogInformation("GetAddressesByFilter returned {Count} addresses.", result.Count);
            return result;
        }

        public override async Task<AddressModel> CreateAddress(AddressModel address)
        {
            _logger.LogInformation("Calling CreateAddress with details: {@AddressModel}", address);
            var result = await base.CreateAddress(address);
            _logger.LogInformation("CreateAddress created: {@AddressModel}", result);
            return result;
        }

        public override async Task<AddressModel> UpdateAddress(AddressModel address)
        {
            _logger.LogInformation("Calling UpdateAddress with details: {@AddressModel}", address);
            var result = await base.UpdateAddress(address);
            _logger.LogInformation("UpdateAddress updated to: {@AddressModel}", result);
            return result;
        }

        public override async Task<bool> DeleteAddress(Guid id)
        {
            _logger.LogInformation("Calling DeleteAddress with ID: {Id}", id);
            var result = await base.DeleteAddress(id);
            _logger.LogInformation("DeleteAddress result: {Result}", result);
            return result;
        }
    }
}
