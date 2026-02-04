using Microsoft.Extensions.Options;
using System.Text.Json;

// Copyright (c) 2019 Blazored
// Modified by dsun on 2026
// MIT License
namespace MetaFrm.Razor.Storage
{
    internal class SystemTextJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerOptions _options;

        public SystemTextJsonSerializer(IOptions<StorageOptions> options)
        {
            _options = options.Value.JsonSerializerOptions;
        }

        public SystemTextJsonSerializer(StorageOptions localStorageOptions)
        {
            _options = localStorageOptions.JsonSerializerOptions;
        }

        public T? Deserialize<T>(string data) 
            => JsonSerializer.Deserialize<T>(data, _options);

        public string Serialize<T>(T data)
            => JsonSerializer.Serialize(data, _options);
    }
}