﻿using Decorator.ClassLibrary.Extensions;
using Decorator.DataAccessLayer.Data;
using Decorator.DataAccessLayer.Decorators;
using Decorator.DataAccessLayer.Repositories;
using Decorator.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Decorator.DataAccessLayer.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseLayer(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.Decorate<IAddressRepository, LoggingAddressRepositoryDecorator>();

            services.AddDbContext<AddressDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            return services;

        }
    }
}
