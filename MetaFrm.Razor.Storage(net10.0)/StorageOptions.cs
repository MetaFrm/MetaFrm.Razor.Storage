using System.Text.Json;

namespace MetaFrm.Razor.Storage
{
    /// <summary>
    /// Provides configuration options for storage operations, including JSON serialization settings.
    /// </summary>
    public class StorageOptions
    {
        /// <summary>
        /// Gets or sets the options to use when serializing and deserializing JSON data.
        /// </summary>
        public JsonSerializerOptions JsonSerializerOptions { get; set; } = new();
    }
}