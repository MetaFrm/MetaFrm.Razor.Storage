using System.Text.Json;

namespace MetaFrm.Razor.Storage.Session
{
    internal class SessionStorageService(ISessionStorageProvider storageProvider, IJsonSerializer serializer) : ISessionStorageService, ISyncSessionStorageService
    {
        public ValueTask RemoveItemsAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default)
            => storageProvider.RemoveItemsAsync(keys, cancellationToken);

        public async ValueTask SetItemAsync<T>(string key, T data, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var e = await RaiseOnChangingAsync(key, data).ConfigureAwait(false);

            if (e.Cancel)
                return;

            var serialisedData = serializer.Serialize(data);
            await storageProvider.SetItemAsync(key, serialisedData, cancellationToken).ConfigureAwait(false);

            RaiseOnChanged(key, e.OldValue, data);
        }

        public async ValueTask SetItemAsStringAsync(string key, string data, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            ArgumentNullException.ThrowIfNull(data);

            var e = await RaiseOnChangingAsync(key, data).ConfigureAwait(false);

            if (e.Cancel)
                return;

            await storageProvider.SetItemAsync(key, data, cancellationToken).ConfigureAwait(false);

            RaiseOnChanged(key, e.OldValue, data);
        }

        public async ValueTask<T?> GetItemAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var serialisedData = await storageProvider.GetItemAsync(key, cancellationToken).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(serialisedData))
                return default;

            try
            {
                return serializer.Deserialize<T>(serialisedData);
            }
            catch (JsonException e) when (e.Path == "$" && typeof(T) == typeof(string))
            {
                // For backward compatibility return the plain string.
                // On the next save a correct value will be stored and this Exception will not happen again, for this 'key'
                return (T)(object)serialisedData;
            }
        }

        public ValueTask<string?> GetItemAsStringAsync(string key, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            return storageProvider.GetItemAsync(key, cancellationToken);
        }

        public ValueTask RemoveItemAsync(string key, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            return storageProvider.RemoveItemAsync(key, cancellationToken);
        }

        public ValueTask ClearAsync(CancellationToken cancellationToken = default)
            => storageProvider.ClearAsync(cancellationToken);

        public ValueTask<int> LengthAsync(CancellationToken cancellationToken = default)
            => storageProvider.LengthAsync(cancellationToken);

        public ValueTask<string?> KeyAsync(int index, CancellationToken cancellationToken = default)
            => storageProvider.KeyAsync(index, cancellationToken);

        public ValueTask<IEnumerable<string>> KeysAsync(CancellationToken cancellationToken = default)
            => storageProvider.KeysAsync(cancellationToken);

        public ValueTask<bool> ContainKeyAsync(string key, CancellationToken cancellationToken = default)
            => storageProvider.ContainKeyAsync(key, cancellationToken);

        public void SetItem<T>(string key, T data)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var e = RaiseOnChangingSync(key, data);

            if (e.Cancel)
                return;

            var serialisedData = serializer.Serialize(data);
            storageProvider.SetItem(key, serialisedData);

            RaiseOnChanged(key, e.OldValue, data);
        }

        public void SetItemAsString(string key, string data)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            ArgumentNullException.ThrowIfNull(data);

            var e = RaiseOnChangingSync(key, data);

            if (e.Cancel)
                return;

            storageProvider.SetItem(key, data);

            RaiseOnChanged(key, e.OldValue, data);
        }

        public T? GetItem<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var serialisedData = storageProvider.GetItem(key);

            if (string.IsNullOrWhiteSpace(serialisedData))
                return default;

            try
            {
                return serializer.Deserialize<T>(serialisedData);
            }
            catch (JsonException e) when (e.Path == "$" && typeof(T) == typeof(string))
            {
                // For backward compatibility return the plain string.
                // On the next save a correct value will be stored and this Exception will not happen again, for this 'key'
                return (T)(object)serialisedData;
            }
        }

        public string? GetItemAsString(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            return storageProvider.GetItem(key);
        }

        public void RemoveItem(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            storageProvider.RemoveItem(key);
        }

        public void RemoveItems(IEnumerable<string> keys)
        {
            ArgumentNullException.ThrowIfNull(keys);

            foreach (var key in keys)
            {
                storageProvider.RemoveItem(key);
            }
        }

        public void Clear()
            => storageProvider.Clear();

        public int Length()
            => storageProvider.Length();

        public string? Key(int index)
            => storageProvider.Key(index);

        public IEnumerable<string> Keys()
            => storageProvider.Keys();

        public bool ContainKey(string key)
            => storageProvider.ContainKey(key);

        public event EventHandler<ChangingEventArgs>? Changing;
        private async Task<ChangingEventArgs> RaiseOnChangingAsync(string key, object? data)
        {
            var e = new ChangingEventArgs
            {
                Key = key,
                OldValue = await GetItemInternalAsync<object>(key).ConfigureAwait(false),
                NewValue = data
            };

            Changing?.Invoke(this, e);

            return e;
        }

        private ChangingEventArgs RaiseOnChangingSync(string key, object? data)
        {
            var e = new ChangingEventArgs
            {
                Key = key,
                OldValue = GetItemInternal(key),
                NewValue = data
            };

            Changing?.Invoke(this, e);

            return e;
        }

        private async Task<T?> GetItemInternalAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var serialisedData = await storageProvider.GetItemAsync(key).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(serialisedData))
                return default;
            try
            {
                return serializer.Deserialize<T>(serialisedData);
            }
            catch (JsonException)
            {
                return (T)(object)serialisedData;
            }
        }

        private object? GetItemInternal(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var serialisedData = storageProvider.GetItem(key);

            if (string.IsNullOrWhiteSpace(serialisedData))
                return default;

            try
            {
                return serializer.Deserialize<object>(serialisedData);
            }
            catch (JsonException)
            {
                return serialisedData;
            }
        }

        public event EventHandler<ChangedEventArgs>? Changed;
        private void RaiseOnChanged(string key, object? oldValue, object? data)
        {
            var e = new ChangedEventArgs
            {
                Key = key,
                OldValue = oldValue,
                NewValue = data
            };

            Changed?.Invoke(this, e);
        }
    }
}