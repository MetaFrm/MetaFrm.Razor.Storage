// Copyright (c) 2019 Blazored
// Modified by dsun on 2026
// MIT License
namespace MetaFrm.Razor.Storage.Local
{
    /// <summary>
    /// Defines a contract for asynchronous interaction with browser local storage, including methods for storing,
    /// retrieving, removing, and enumerating data, as well as events for change notifications.
    /// </summary>
    /// <remarks>Implementations of this interface enable .NET applications to persist and manage data in the
    /// browser's local storage in a type-safe and asynchronous manner. All operations are asynchronous and support
    /// cancellation via a CancellationToken. Events are provided to notify subscribers when local storage is about to
    /// change or has changed, which can be useful for synchronizing application state. This interface is typically used
    /// in Blazor or other web-based .NET applications that require client-side persistence.</remarks>
    public interface ILocalStorageService : IStorageService
    {
    }
}