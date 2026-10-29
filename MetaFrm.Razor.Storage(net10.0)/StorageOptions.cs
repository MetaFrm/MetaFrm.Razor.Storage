using System.Text.Json;

// Copyright (c) 2019 Blazored
// Modified by dsun on 2026
// MIT License
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