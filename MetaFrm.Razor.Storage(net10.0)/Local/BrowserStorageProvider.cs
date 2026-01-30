using Microsoft.JSInterop;

namespace MetaFrm.Razor.Storage.Local
{
    internal class BrowserStorageProvider(IJSRuntime jSRuntime) : BrowserStorageProviderBase(jSRuntime), ILocalStorageProvider
    {
        public async ValueTask<string?> GetItemAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                return await JSRuntime.InvokeAsync<string?>("localStorage.getItem", cancellationToken, key);
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
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", cancellationToken, key, data);
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
    }
}