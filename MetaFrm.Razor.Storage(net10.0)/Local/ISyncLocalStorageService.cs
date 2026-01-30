namespace MetaFrm.Razor.Storage.Local
{
    /// <summary>
    /// Defines synchronous methods for interacting with browser local storage, enabling storage, retrieval, and
    /// management of key-value data within the local client environment.
    /// </summary>
    /// <remarks>This interface provides a set of methods for managing data in local storage, including
    /// adding, retrieving, removing, and enumerating items. It also exposes events to notify subscribers when changes
    /// to the storage are about to occur or have occurred. Implementations are expected to handle serialization and
    /// deserialization of complex types where applicable. Local storage is typically limited in size and is specific to
    /// the user's browser and device. This interface is not thread-safe; callers should ensure appropriate
    /// synchronization if accessed concurrently.</remarks>
    public interface ISyncLocalStorageService : ISyncStorageService
    {
    }
}