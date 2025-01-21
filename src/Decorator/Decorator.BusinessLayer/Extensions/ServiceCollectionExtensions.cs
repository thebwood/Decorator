using Microsoft.Extensions.DependencyInjection;
using System;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Decorate<TService, TDecorator>(this IServiceCollection services)
        where TService : class
        where TDecorator : class, TService
    {
        // Find the existing service registration
        var descriptor = services.FirstOrDefault(s => s.ServiceType == typeof(TService));

        if (descriptor == null)
        {
            throw new InvalidOperationException($"Service of type {typeof(TService).Name} is not registered.");
        }

        // Remove the existing registration
        services.Remove(descriptor);

        // Add the decorator with the original implementation as a dependency
        var factory = descriptor.ImplementationFactory;

        services.AddTransient<TService>(provider =>
        {
            var original = factory != null
                ? (TService)factory(provider)
                : (TService)ActivatorUtilities.CreateInstance(provider, descriptor.ImplementationType!);

            return ActivatorUtilities.CreateInstance<TDecorator>(provider, original);
        });

        return services;
    }
}
