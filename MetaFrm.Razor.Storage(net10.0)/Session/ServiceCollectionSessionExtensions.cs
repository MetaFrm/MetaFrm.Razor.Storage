using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace MetaFrm.Razor.Storage.Session
{
    /// <summary>
    /// Provides extension methods for registering Blazored SessionStorage services with an IServiceCollection.
    /// </summary>
    /// <remarks>These extension methods enable the configuration and registration of Blazored SessionStorage
    /// and related services for use in Blazor applications. Methods are provided for both scoped and singleton service
    /// lifetimes, allowing integration with Blazor Server and Blazor WebAssembly hosting models. Use singleton
    /// registration methods only in Blazor WebAssembly applications, as singleton services are not suitable for Blazor
    /// Server and may result in unexpected behavior.</remarks>
    [ExcludeFromCodeCoverage]
    internal static class ServiceCollectionSessionExtensions
    {
        internal static IServiceCollection AddBlazoredSessionStorage(this IServiceCollection services, Action<StorageOptions>? configure)
        {
            if (!services.Any(x => x.ServiceType == typeof(IJsonSerializer)))
                services.AddScoped<IJsonSerializer, SystemTextJsonSerializer>();

            return services
                .AddScoped<ISessionStorageProvider, BrowserStorageProvider>()
                .AddScoped<ISessionStorageService, SessionStorageService>()
                .AddScoped<ISyncSessionStorageService, SessionStorageService>()
                .Configure<StorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }

        /// <summary>
        /// Registers the Blazored SessionStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        internal static IServiceCollection AddBlazoredSessionStorageAsSingleton(this IServiceCollection services, Action<StorageOptions>? configure)
        {
            if (!services.Any(x => x.ServiceType == typeof(IJsonSerializer)))
                services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();

            return services.AddSingleton<ISessionStorageProvider, BrowserStorageProvider>()
                .AddSingleton<ISessionStorageService, SessionStorageService>()
                .AddSingleton<ISyncSessionStorageService, SessionStorageService>()
                .Configure<StorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}