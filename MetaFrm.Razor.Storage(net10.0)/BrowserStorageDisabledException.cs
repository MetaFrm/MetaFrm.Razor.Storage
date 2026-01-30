namespace MetaFrm.Razor.Storage
{
    /// <summary>
    /// The exception that is thrown when browser storage is disabled or unavailable.
    /// </summary>
    /// <remarks>This exception typically indicates that the application attempted to access browser-based
    /// storage (such as localStorage or sessionStorage) but the feature is not enabled or supported in the current
    /// environment. This may occur due to user settings, browser policies, or running in a context where storage is not
    /// accessible (such as private browsing modes or restricted iframes).</remarks>
    public class BrowserStorageDisabledException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BrowserStorageDisabledException class.
        /// </summary>
        /// <remarks>This exception is typically thrown when an operation requires browser storage, but
        /// storage is disabled or unavailable. Use this exception to detect and handle scenarios where browser-based
        /// storage cannot be accessed.</remarks>
        public BrowserStorageDisabledException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BrowserStorageDisabledException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BrowserStorageDisabledException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BrowserStorageDisabledException class with a specified error message and a
        /// reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is
        /// specified.</param>
        public BrowserStorageDisabledException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}