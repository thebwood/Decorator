using Decorator.BusinessLayer.Decorators;
using Decorator.BusinessLayer.Services;
using Decorator.BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Decorator.BusinessLayer.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddTransient<IAddressService, AddressService>();
            services.Decorate<IAddressService, ValidationAddressServiceDecorator>();
            services.Decorate<IAddressService, LoggingAddressServiceDecorator>();
            return services;
        }
    }
}
