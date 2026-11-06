using System.Diagnostics.CodeAnalysis;

// Copyright (c) 2019 Blazored
// Modified by dsun on 2026
// MIT License
namespace MetaFrm.Razor.Storage
{
    /// <summary>
    /// Provides data for events that signal a change to a value associated with a specific key.
    /// </summary>
    /// <remarks>This class is typically used as the event data for change notification events, such as when a
    /// value in a collection or configuration source is modified. It contains the key identifying the changed item, as
    /// well as the old and new values.</remarks>
    [ExcludeFromCodeCoverage]
    public class ChangedEventArgs
    {
        /// <summary>
        /// Gets or sets the unique key that identifies this instance.
        /// </summary>
        public required string Key { get; set; } = null!; // Since .NET 6 is supported, `required` is not available yet

        /// <summary>
        /// Gets or sets the previous value before the most recent change.
        /// </summary>
        public object? OldValue { get; set; }

        /// <summary>
        /// Gets or sets the new value associated with the current operation.
        /// </summary>
        public object? NewValue { get; set; }
    }
}