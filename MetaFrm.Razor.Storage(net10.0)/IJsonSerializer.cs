// Copyright (c) 2019 Blazored
// Modified by dsun on 2026
// MIT License
namespace MetaFrm.Razor.Storage
{
    /// <summary>
    /// Defines methods to serialize objects to JSON and deserialize JSON to objects.
    /// </summary>
    /// <remarks>Implementations of this interface provide functionality for converting objects to their JSON
    /// string representation and reconstructing objects from JSON. This interface is typically used to abstract JSON
    /// serialization logic, allowing for interchangeable serialization strategies or libraries.</remarks>
    public interface IJsonSerializer
    {
        /// <summary>
        /// Serializes the specified object to a string representation.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="obj">The object to serialize. Cannot be null.</param>
        /// <returns>A string that represents the serialized form of the specified object.</returns>
        string Serialize<T>(T obj);

        /// <summary>
        /// Deserializes the specified text into an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="text">The string containing the serialized representation of the object. Cannot be null.</param>
        /// <returns>An instance of type T deserialized from the specified text, or null if the text represents a null value.</returns>
        T? Deserialize<T>(string text);
    }
}