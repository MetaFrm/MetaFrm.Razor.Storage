using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace MetaFrm.Razor.Storage.Session
{
    internal class BrowserStorageProvider(IJSRuntime jSRuntime) : ISessionStorageProvider
    {
        private const string StorageNotAvailableMessage = "Unable to access the browser storage. This is most likely due to the browser settings.";

        private readonly IJSInProcessRuntime? JSInProcessRuntime = jSRuntime as IJSInProcessRuntime;

        public void Clear()
        {
            CheckForInProcessRuntime();
            try
            {
                JSInProcessRuntime.InvokeVoid("sessionStorage.clear");
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public async ValueTask ClearAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await jSRuntime.InvokeVoidAsync("sessionStorage.clear", cancellationToken);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public bool ContainKey(string key)
        {
            CheckForInProcessRuntime();
            try
            {
                return JSInProcessRuntime.Invoke<bool>("sessionStorage.hasOwnProperty", key);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public async ValueTask<bool> ContainKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                return await jSRuntime.InvokeAsync<bool>("sessionStorage.hasOwnProperty", cancellationToken, key);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public string GetItem(string key)
        {
            CheckForInProcessRuntime();
            try
            {
                return JSInProcessRuntime.Invoke<string>("sessionStorage.getItem", key);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public async ValueTask<string?> GetItemAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                return await jSRuntime.InvokeAsync<string>("sessionStorage.getItem", cancellationToken, key);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public string Key(int index)
        {
            CheckForInProcessRuntime();
            try
            {
                return JSInProcessRuntime.Invoke<string>("sessionStorage.key", index);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public async ValueTask<string?> KeyAsync(int index, CancellationToken cancellationToken = default)
        {
            try
            {
                return await jSRuntime.InvokeAsync<string>("sessionStorage.key", cancellationToken, index);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public IEnumerable<string> Keys()
        {
            CheckForInProcessRuntime();
            try
            {
                return JSInProcessRuntime.Invoke<IEnumerable<string>>("eval", "Object.keys(sessionStorage)");
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public async ValueTask<IEnumerable<string>> KeysAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await jSRuntime.InvokeAsync<IEnumerable<string>>("eval", cancellationToken, "Object.keys(sessionStorage)");
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public int Length()
        {
            CheckForInProcessRuntime();
            try
            {
                return JSInProcessRuntime.Invoke<int>("eval", "sessionStorage.length");
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public async ValueTask<int> LengthAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await jSRuntime.InvokeAsync<int>("eval", cancellationToken, "sessionStorage.length");
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public void RemoveItem(string key)
        {
            CheckForInProcessRuntime();
            try
            {
                JSInProcessRuntime.InvokeVoid("sessionStorage.removeItem", key);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public async ValueTask RemoveItemAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await jSRuntime.InvokeVoidAsync("sessionStorage.removeItem", cancellationToken, key);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public void RemoveItems(IEnumerable<string> keys)
        {
            CheckForInProcessRuntime();
            try
            {
                foreach (var key in keys)
                {
                    JSInProcessRuntime.InvokeVoid("sessionStorage.removeItem", key);
                }
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public async ValueTask RemoveItemsAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var key in keys)
                {
                    await jSRuntime.InvokeVoidAsync("sessionStorage.removeItem", cancellationToken, key);
                }
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public void SetItem(string key, string data)
        {
            CheckForInProcessRuntime();
            try
            {
                JSInProcessRuntime.InvokeVoid("sessionStorage.setItem", key, data);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        public async ValueTask SetItemAsync(string key, string data, CancellationToken cancellationToken = default)
        {
            try
            {
                await jSRuntime.InvokeVoidAsync("sessionStorage.setItem", cancellationToken, key, data);
            }
            catch (Exception exception)
            {
                if (IsStorageDisabledException(exception))
                {
                    throw new BrowserStorageDisabledException(StorageNotAvailableMessage, exception);
                }

                throw;
            }
        }

        [MemberNotNull(nameof(JSInProcessRuntime))]
        private void CheckForInProcessRuntime()
        {
            if (JSInProcessRuntime == null)
                throw new InvalidOperationException("IJSInProcessRuntime not available");
        }

        private static bool IsStorageDisabledException(Exception exception)
            => exception.Message.Contains("Failed to read the 'sessionStorage' property from 'Window'");
    }
}