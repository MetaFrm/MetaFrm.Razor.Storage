using System.Diagnostics.CodeAnalysis;

namespace MetaFrm.Razor.Storage
{
    /// <summary>
    /// Provides data for events that occur before a change is committed, allowing the change to be canceled.
    /// </summary>
    /// <remarks>Use this class with events that notify listeners of a pending change, such as before
    /// modifying a collection or property. Setting the Cancel property to <see langword="true"/> will prevent the
    /// change from being applied.</remarks>
    [ExcludeFromCodeCoverage]
    public class ChangingEventArgs : ChangedEventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether the current operation should be canceled.
        /// </summary>
        public bool Cancel { get; set; }
    }
}