namespace MetaFrm.Razor.Storage
{
    /// <summary>
    /// Defines synchronous methods for interacting with browser local/session storage, enabling storage, retrieval, and
    /// management of key-value data within the local/session client environment.
    /// </summary>
    /// <remarks>This interface provides a set of methods for managing data in local/session storage, including
    /// adding, retrieving, removing, and enumerating items. It also exposes events to notify subscribers when changes
    /// to the storage are about to occur or have occurred. Implementations are expected to handle serialization and
    /// deserialization of complex types where applicable. Local/session storage is typically limited in size and is specific to
    /// the user's browser and device. This interface is not thread-safe; callers should ensure appropriate
    /// synchronization if accessed concurrently.</remarks>
    public interface ISyncStorageService
    {
        /// <summary>
        /// Clears all data from local/session storage.
        /// </summary>
        void Clear();

        /// <summary>
        /// Retrieve the specified data from local/session storage as a <typeparamref name="T"/>.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the local/session storage slot to use</param>
        /// <returns>The data from the specified <paramref name="key"/> as a <typeparamref name="T"/></returns>
        T? GetItem<T>(string key);

        /// <summary>
        /// Retrieve the specified data from local/session storage as a <see cref="string"/>.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
        /// <returns>The data associated with the specified <paramref name="key"/> as a <see cref="string"/></returns>
        string? GetItemAsString(string key);

        /// <summary>
        /// Return the name of the key at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The name of the key at the specified <paramref name="index"/></returns>
        string? Key(int index);

        /// <summary>
        /// Checks if the <paramref name="key"/> exists in local/session storage, but does not check its value.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
        /// <returns>Boolean indicating if the specified <paramref name="key"/> exists</returns>
        bool ContainKey(string key);

        /// <summary>
        /// The number of items stored in local/session storage.
        /// </summary>
        /// <returns>The number of items stored in local/session storage</returns>
        int Length();

        /// <summary>
        /// Get the keys of all items stored in local/session storage.
        /// </summary>
        /// <returns>The keys of all items stored in local/session storage</returns>
        IEnumerable<string> Keys();

        /// <summary>
        /// Remove the data with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to remove</param>
        void RemoveItem(string key);

        /// <summary>
        /// Removes a collection of <paramref name="keys"/>.
        /// </summary>
        /// <param name="keys">A IEnumerable collection of strings specifying the name of the storage slot to remove</param>
        void RemoveItems(IEnumerable<string> keys);

        /// <summary>
        /// Sets or updates the <paramref name="data"/> in local/session storage with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
        /// <param name="data">The data to be saved</param>
        void SetItem<T>(string key, T data);

        /// <summary>
        /// Sets or updates the <paramref name="data"/> in local/session storage with the specified <paramref name="key"/>. Does not serialize the value before storing.
        /// </summary>
        /// <param name="key">A <see cref="string"/> value specifying the name of the storage slot to use</param>
        /// <param name="data">The string to be saved</param>
        /// <returns></returns>
        void SetItemAsString(string key, string data);

        /// <summary>
        /// Occurs when the object is about to change, allowing handlers to respond or cancel the operation.
        /// </summary>
        /// <remarks>Subscribe to this event to perform validation, logging, or to prevent the change by
        /// setting properties on the event arguments. The event is raised before the change is applied, giving handlers
        /// an opportunity to inspect or modify the pending operation.</remarks>
        event EventHandler<ChangingEventArgs> Changing;
        /// <summary>
        /// Occurs when the underlying data or state has changed.
        /// </summary>
        /// <remarks>Subscribers are notified whenever a change is detected. The <see
        /// cref="ChangedEventArgs"/> parameter provides details about the change.</remarks>
        event EventHandler<ChangedEventArgs> Changed;
    }
}