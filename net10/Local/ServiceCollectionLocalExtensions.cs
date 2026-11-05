using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

// Copyright (c) 2019 Blazored
// Modified by dsun on 2026
// MIT License
namespace MetaFrm.Razor.Storage.Local
{
    /// <summary>
    /// Provides extension methods for registering Blazored LocalStorage services with an IServiceCollection.
    /// </summary>
    /// <remarks>These extension methods enable the configuration and registration of Blazored LocalStorage
    /// and related services for use in Blazor applications. Methods are provided for both scoped and singleton service
    /// lifetimes, allowing integration with Blazor Server and Blazor WebAssembly hosting models. Use singleton
    /// registration methods only in Blazor WebAssembly applications, as singleton services are not suitable for Blazor
    /// Server and may result in unexpected behavior.</remarks>
    [ExcludeFromCodeCoverage]
    internal static class ServiceCollectionLocalExtensions
    {
        /// <summary>
        /// Adds Blazored LocalStorage services to the specified service collection with optional configuration.
        /// </summary>
        /// <remarks>Call this method in your application's startup code to enable dependency injection
        /// for Blazored LocalStorage. This method registers the required services with a scoped lifetime.</remarks>
        /// <param name="services">The service collection to which the Blazored LocalStorage services will be added. Cannot be null.</param>
        /// <param name="configure">An optional action to configure the LocalStorage options. If null, default options are used.</param>
        /// <returns>The same instance of <see cref="IServiceCollection"/> that was provided, to support method chaining.</returns>
        internal static IServiceCollection AddBlazoredLocalStorage(this IServiceCollection services, Action<StorageOptions>? configure)
        {
            services.TryAddScoped<ILocalStorageProvider, BrowserStorageProvider>();
            AddServices(services, configure);
            return services;
        }

        /// <summary>
        /// Adds Blazored local storage streaming services to the specified service collection.
        /// </summary>
        /// <remarks>This method registers the required services for using Blazored local storage with
        /// streaming support in a Blazor application. Call this method during application startup to enable local
        /// storage streaming functionality.</remarks>
        /// <param name="services">The service collection to which the local storage streaming services will be added.</param>
        /// <param name="configure">An optional action to configure the local storage options. If null, default options are used.</param>
        /// <returns>The same service collection instance so that additional calls can be chained.</returns>
        internal static IServiceCollection AddBlazoredLocalStorageStreaming(this IServiceCollection services, Action<StorageOptions>? configure)
        {
            services.TryAddScoped<ILocalStorageProvider, BrowserStreamingStorageProvider>();
            AddServices(services, configure);
            return services;
        }

        private static void AddServices(IServiceCollection services, Action<StorageOptions>? configure)
        {

            if (!services.Any(x => x.ServiceType == typeof(IJsonSerializer)))
                services.TryAddScoped<IJsonSerializer, SystemTextJsonSerializer>();

            services.TryAddScoped<ILocalStorageService, LocalStorageService>();
            services.TryAddScoped<ISyncLocalStorageService, LocalStorageService>();
            if (services.All(serviceDescriptor => serviceDescriptor.ServiceType != typeof(IConfigureOptions<StorageOptions>)))
            {
                services.Configure<StorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
            }
        }

        /// <summary>
        /// Registers the Blazored LocalStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        internal static IServiceCollection AddBlazoredLocalStorageAsSingleton(this IServiceCollection services, Action<StorageOptions>? configure)
        {
            if (!services.Any(x => x.ServiceType == typeof(IJsonSerializer)))
                services.TryAddSingleton<IJsonSerializer, SystemTextJsonSerializer>();

            services.TryAddSingleton<ILocalStorageProvider, BrowserStorageProvider>();
            services.TryAddSingleton<ILocalStorageService, LocalStorageService>();
            services.TryAddSingleton<ISyncLocalStorageService, LocalStorageService>();
            if (services.All(serviceDescriptor => serviceDescriptor.ServiceType != typeof(IConfigureOptions<StorageOptions>)))
            {
                services.Configure<StorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
            }
            return services;
        }
    }
}