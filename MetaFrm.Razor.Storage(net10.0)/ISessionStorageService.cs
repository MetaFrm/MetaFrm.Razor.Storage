using Microsoft.JSInterop;

namespace MetaFrm.Razor.Storage
{
    /// <summary>
    /// Defines a contract for asynchronous interaction with browser local/session storage, including methods for storing,
    /// retrieving, removing, and enumerating data, as well as events for change notifications.
    /// </summary>
    /// <remarks>Implementations of this interface enable .NET applications to persist and manage data in the
    /// browser's local/session storage in a type-safe and asynchronous manner. All operations are asynchronous and support
    /// cancellation via a CancellationToken. Events are provided to notify subscribers when local/session storage is about to
    /// change or has changed, which can be useful for synchronizing application state. This interface is typically used
    /// in Blazor or other web-based .NET applications that require client-side persistence.</remarks>
    public interface IStorageService
    {
        /// <summary>
        /// Clears all data from local/session storage.
        /// </summary>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask ClearAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the specified data from local/session storage and deseralise it to the specfied type.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the local/session storage slot to use</param>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask<T?> GetItemAsync<T>(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve the specified data from local/session storage as a <see cref="string"/>.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask<string?> GetItemAsStringAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Return the name of the key at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask<string?> KeyAsync(int index, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a collection of strings representing the names of the keys in the local/Session storage.
        /// </summary>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask<IEnumerable<string>> KeysAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if the <paramref name="key"/> exists in local/session storage, but does not check its value.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask<bool> ContainKeyAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// The number of items stored in local/session storage.
        /// </summary>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask<int> LengthAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove the data with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask RemoveItemAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a collection of <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">A IEnumerable collection of strings specifying the name of the storage slot to remove</param>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask RemoveItemsAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets or updates the <paramref name="data"/> in local/session storage with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
        /// <param name="data">The data to be saved</param>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask SetItemAsync<T>(string key, T data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets or updates the <paramref name="data"/> in local/session storage with the specified <paramref name="key"/>. Does not serialize the value before storing.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
        /// <param name="data">The string to be saved</param>
        /// <param name="cancellationToken">
        /// A cancellation token to signal the cancellation of the operation. Specifying this parameter will override any default cancellations such as due to timeouts
        /// (<see cref="JSRuntime.DefaultAsyncTimeout"/>) from being applied.
        /// </param>
        /// <returns>A <see cref="ValueTask"/> representing the completion of the operation.</returns>
        ValueTask SetItemAsStringAsync(string key, string data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Occurs when the object is about to change, allowing handlers to respond or cancel the operation.
        /// </summary>
        /// <remarks>Handlers can inspect the change details and may be able to prevent the change by
        /// setting properties on the event arguments, depending on the implementation. Subscribe to this event to
        /// perform validation or veto changes before they are applied.</remarks>
        event EventHandler<ChangingEventArgs> Changing;
        /// <summary>
        /// Occurs when the underlying data or state has changed.
        /// </summary>
        /// <remarks>Subscribers can handle this event to respond to changes in the associated object. The
        /// event provides a <see cref="ChangedEventArgs"/> instance containing details about the change.</remarks>
        event EventHandler<ChangedEventArgs> Changed;
    }
}