using MetaFrm.Razor.Storage.Local;
using MetaFrm.Razor.Storage.Session;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

// Copyright (c) 2019 Blazored
// Modified by dsun on 2026
// MIT License
namespace MetaFrm.Razor.Storage
{
    /// <summary>
    /// Provides extension methods for registering Blazored LocalStorage/SessionStorage services with an IServiceCollection.
    /// </summary>
    /// <remarks>These extension methods enable the configuration and registration of Blazored LocalStorage/SessionStorage
    /// and related services for use in Blazor applications. Methods are provided for both scoped and singleton service
    /// lifetimes, allowing integration with Blazor Server and Blazor WebAssembly hosting models. Use singleton
    /// registration methods only in Blazor WebAssembly applications, as singleton services are not suitable for Blazor
    /// Server and may result in unexpected behavior.</remarks>
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Blazored LocalStorage/SessionStorage services to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to which the Blazored LocalStorage/SessionStorage services will be added. Cannot be null.</param>
        /// <returns>The original service collection with Blazored LocalStorage/SessionStorage services registered.</returns>
        public static IServiceCollection AddStorage(this IServiceCollection services) => AddStorage(services, null);
        /// <summary>
        /// Adds Blazored LocalStorage/SessionStorage services to the specified service collection with optional configuration.
        /// </summary>
        /// <remarks>Call this method in your application's startup code to enable dependency injection
        /// for Blazored LocalStorage/SessionStorage. This method registers the required services with a scoped lifetime.</remarks>
        /// <param name="services">The service collection to which the Blazored LocalStorage/SessionStorage services will be added. Cannot be null.</param>
        /// <param name="configure">An optional action to configure the LocalStorage/SessionStorage options. If null, default options are used.</param>
        /// <returns>The same instance of <see cref="IServiceCollection"/> that was provided, to support method chaining.</returns>
        public static IServiceCollection AddStorage(this IServiceCollection services, Action<StorageOptions>? configure)
        { 
            AddLocalStorage(services, configure);
            AddSessionStorage(services, configure);

            return services;
        }

        /// <summary>
        /// Adds Blazored LocalStorage(streaming)/SessionStorage services to the specified service collection.
        /// </summary>
        /// <remarks>This method registers the required services for Blazored LocalStorage(streaming)/SessionStorage
        /// support. Call this method during application startup to enable streaming features for local storage in
        /// Blazor applications.</remarks>
        /// <param name="services">The service collection to which the Blazored LocalStorage(streaming)/SessionStorage services will be added. Cannot be null.</param>
        /// <returns>The same instance of <see cref="IServiceCollection"/> that was provided, to support method chaining.</returns>
        public static IServiceCollection AddStorageStreaming(this IServiceCollection services) => AddStorageStreaming(services, null);
        /// <summary>
        /// Adds Blazored local(streaming)/Session storage streaming services to the specified service collection.
        /// </summary>
        /// <remarks>This method registers the required services for using Blazored local(streaming)/Session storage with
        /// streaming support in a Blazor application. Call this method during application startup to enable local(streaming)/Session
        /// storage streaming functionality.</remarks>
        /// <param name="services">The service collection to which the local(streaming)/Session storage streaming services will be added.</param>
        /// <param name="configure">An optional action to configure the local(streaming)/Session storage options. If null, default options are used.</param>
        /// <returns>The same service collection instance so that additional calls can be chained.</returns>
        public static IServiceCollection AddStorageStreaming(this IServiceCollection services, Action<StorageOptions>? configure)
        { 
            AddLocalStorageStreaming(services, configure);
            AddSessionStorage(services, configure);

            return services;
        }

        /// <summary>
        /// Registers the Blazored LocalStorage/SessionStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddStorageAsSingleton(this IServiceCollection services) => AddStorageAsSingleton(services, null);
        /// <summary>
        /// Registers the Blazored LocalStorage/SessionStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddStorageAsSingleton(this IServiceCollection services, Action<StorageOptions>? configure)
        {
            AddLocalStorageAsSingleton(services, configure);
            AddSessionStorageAsSingleton(services, configure);

            return services;
        }


        /// <summary>
        /// Adds the Blazored LocalStorage services to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to which the Blazored LocalStorage services will be added. Cannot be null.</param>
        /// <returns>The original service collection with Blazored LocalStorage services registered.</returns>
        public static IServiceCollection AddLocalStorage(this IServiceCollection services) => AddLocalStorage(services, null);
        /// <summary>
        /// Adds Blazored LocalStorage services to the specified service collection with optional configuration.
        /// </summary>
        /// <remarks>Call this method in your application's startup code to enable dependency injection
        /// for Blazored LocalStorage. This method registers the required services with a scoped lifetime.</remarks>
        /// <param name="services">The service collection to which the Blazored LocalStorage services will be added. Cannot be null.</param>
        /// <param name="configure">An optional action to configure the LocalStorage options. If null, default options are used.</param>
        /// <returns>The same instance of <see cref="IServiceCollection"/> that was provided, to support method chaining.</returns>
        public static IServiceCollection AddLocalStorage(this IServiceCollection services, Action<StorageOptions>? configure) => ServiceCollectionLocalExtensions.AddBlazoredLocalStorage(services, configure);

        /// <summary>
        /// Adds Blazored LocalStorage streaming services to the specified service collection.
        /// </summary>
        /// <remarks>This method registers the required services for Blazored LocalStorage streaming
        /// support. Call this method during application startup to enable streaming features for local storage in
        /// Blazor applications.</remarks>
        /// <param name="services">The service collection to which the Blazored LocalStorage streaming services will be added. Cannot be null.</param>
        /// <returns>The same instance of <see cref="IServiceCollection"/> that was provided, to support method chaining.</returns>
        public static IServiceCollection AddLocalStorageStreaming(this IServiceCollection services) => AddLocalStorageStreaming(services, null);
        /// <summary>
        /// Adds Blazored local storage streaming services to the specified service collection.
        /// </summary>
        /// <remarks>This method registers the required services for using Blazored local storage with
        /// streaming support in a Blazor application. Call this method during application startup to enable local
        /// storage streaming functionality.</remarks>
        /// <param name="services">The service collection to which the local storage streaming services will be added.</param>
        /// <param name="configure">An optional action to configure the local storage options. If null, default options are used.</param>
        /// <returns>The same service collection instance so that additional calls can be chained.</returns>
        public static IServiceCollection AddLocalStorageStreaming(this IServiceCollection services, Action<StorageOptions>? configure) => ServiceCollectionLocalExtensions.AddBlazoredLocalStorageStreaming(services, configure);

        /// <summary>
        /// Registers the Blazored LocalStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddLocalStorageAsSingleton(this IServiceCollection services) => AddLocalStorageAsSingleton(services, null);
        /// <summary>
        /// Registers the Blazored LocalStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddLocalStorageAsSingleton(this IServiceCollection services, Action<StorageOptions>? configure) => ServiceCollectionLocalExtensions.AddBlazoredLocalStorageAsSingleton(services, configure);


        /// <summary>
        /// Adds the Blazored SessionStorage services to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to which the Blazored SessionStorage services will be added. Cannot be null.</param>
        /// <returns>The original service collection with Blazored SessionStorage services registered.</returns>
        public static IServiceCollection AddSessionStorage(this IServiceCollection services) => AddSessionStorage(services, null);
        /// <summary>
        /// Adds the Blazored SessionStorage services to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to which the Blazored SessionStorage services will be added. Cannot be null.</param>
        /// <param name="configure"></param>
        /// <returns>The original service collection with Blazored SessionStorage services registered.</returns>
        public static IServiceCollection AddSessionStorage(this IServiceCollection services, Action<StorageOptions>? configure) => ServiceCollectionSessionExtensions.AddBlazoredSessionStorage(services, configure);

        /// <summary>
        /// Registers the Blazored SessionStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddSessionStorageAsSingleton(this IServiceCollection services) => AddSessionStorageAsSingleton(services, null);
        /// <summary>
        /// Registers the Blazored SessionStorage services as singletons. This should only be used in Blazor WebAssembly applications.
        /// Using this in Blazor Server applications will cause unexpected and potentially dangerous behaviour. 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddSessionStorageAsSingleton(this IServiceCollection services, Action<StorageOptions>? configure) => ServiceCollectionSessionExtensions.AddBlazoredSessionStorageAsSingleton(services, configure);
    }
}