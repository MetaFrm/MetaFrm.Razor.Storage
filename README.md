# MetaFrm.Razor.Storage

### .Net 10.0
[![build 10.0](https://github.com/MetaFrm/MetaFrm.Razor.Storage/actions/workflows/build_10.0.yml/badge.svg)](https://github.com/MetaFrm/MetaFrm.Razor.Storage/actions/workflows/build_10.0.yml)
[![NuGet version (MetaFrm.Razor.Storage.net10.0)](https://img.shields.io/nuget/v/MetaFrm.Razor.Storage.net10.0)](https://www.nuget.org/packages/MetaFrm.Razor.Storage.net10.0/)
[![NuGet downloads (MetaFrm.Razor.Storage.net10.0)](https://img.shields.io/nuget/dt/MetaFrm.Razor.Storage.net10.0)](https://www.nuget.org/packages/MetaFrm.Razor.Storage.net10.0/)

## Sponsor/Donate
[![Sponsor/Donate](https://i.imgur.com/nyZtCjx.png)](https://www.buymeacoffee.com/autoking)

## Original Project
[Blazored LocalStorage](https://github.com/Blazored/LocalStorage)

[Blazored SessionStorage](https://github.com/Blazored/SessionStorage)

# MetaFramework Razor Storage
Razor LocalStorage/SessionStorage is a library that provides access to the browsers local/session storage APIs for Blazor applications. An additional benefit of using this library is that it will handle serializing and deserializing values when saving or retrieving them.

## Installing

To install the package add the following line to you csproj file replacing x.x.x with the latest version number (found at the top of this file):

```
<PackageReference Include="MetaFrm.Razor.Storage.net10.0" Version="x.x.x" />
```

You can also install via the .NET CLI with the following command:

```
dotnet add package MetaFrm.Razor.Storage.net10.0
```

If you're using Visual Studio you can also install via the built in NuGet package manager.

## Setup LocalStorage/SessionStorage

You will need to register the local/session storage services with the service collection in your _Startup.cs_ file in Blazor Server.

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddStorage();
}
``` 

Or in your _Program.cs_ file in Blazor WebAssembly.

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddStorage();

    await builder.Build().RunAsync();
}
```

## Setup LocalStorage

You will need to register the local storage services with the service collection in your _Startup.cs_ file in Blazor Server.

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddLocalStorage();
}
``` 

Or in your _Program.cs_ file in Blazor WebAssembly.

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddLocalStorage();

    await builder.Build().RunAsync();
}
```

## Setup SessionStorage

You will need to register the session storage services with the service collection in your _Startup.cs_ file in Blazor Server.

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddSessionStorage();
}
``` 

Or in your _Program.cs_ file in Blazor WebAssembly.

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddSessionStorage();

    await builder.Build().RunAsync();
}
```

### Registering services as Singleton (LocalStorage/SessionStorage) - Blazor WebAssembly **ONLY**
99% of developers will want to register Blazored LocalStorage/SessionStorage using the method described above. However, in some very specific scenarios 
developer may have a need to register services as Singleton as apposed to Scoped. This is possible by using the following method:

```csharp
builder.Services.AddStorageAsSingleton();
```

### Registering services as Singleton (LocalStorage) - Blazor WebAssembly **ONLY**
99% of developers will want to register Blazored LocalStorage using the method described above. However, in some very specific scenarios 
developer may have a need to register services as Singleton as apposed to Scoped. This is possible by using the following method:

```csharp
builder.Services.AddLocalStorageAsSingleton();
```

### Registering services as Singleton (SessionStorage) - Blazor WebAssembly **ONLY**
99% of developers will want to register Blazored SessionStorage using the method described above. However, in some very specific scenarios 
developer may have a need to register services as Singleton as apposed to Scoped. This is possible by using the following method:

```csharp
builder.Services.AddSessionStorageAsSingleton();
```

This method will not work with Blazor Server applications as Blazor's JS interop services are registered as Scoped and cannot be injected into Singletons.

### Using JS Interop Streaming
When using interactive components in server-side apps JS Interop calls are limited to the configured SignalR message size (default: 32KB). 
Therefore when attempting to store or retrieve an object larger than this in LocalStorage the call will fail with a SignalR exception. 

The following streaming implementation can be used to remove this limit (you will still be limited by the browser).

Register the streaming local storage service 

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddStorageStreaming();
}
``` 

Add the JavaScript file to your _App.razor_

```html
 <script src="_content/MetaFrm.Razor.Storage.net10.0/Blazored.LocalStorage.js"></script>
```

### Using JS Interop Streaming (LocalStorage)
When using interactive components in server-side apps JS Interop calls are limited to the configured SignalR message size (default: 32KB). 
Therefore when attempting to store or retrieve an object larger than this in LocalStorage the call will fail with a SignalR exception. 

The following streaming implementation can be used to remove this limit (you will still be limited by the browser).

Register the streaming local storage service 

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddLocalStorageStreaming();
}
``` 

Add the JavaScript file to your _App.razor_

```html
 <script src="_content/MetaFrm.Razor.Storage.net10.0/Blazored.LocalStorage.js"></script>
```

## Usage (Blazor WebAssembly)
To use MetaFrm.Razor.Storage in Blazor WebAssembly, inject the `ILocalStorageService`, `ISessionStorageService` per the example below.

```c#
@inject MetaFrm.Razor.Storage.Local.ILocalStorageService local
@inject MetaFrm.Razor.Storage.Session.ISessionStorageService session

@code {

    protected override async Task OnInitializedAsync()
    {
        await local.SetItemAsync("name", "John Smith");
        var name = await local.GetItemAsync<string>("name");
        
        await session.SetItemAsync("name", "John Smith");
        var name1 = await session.GetItemAsync<string>("name");
    }

}
```

With Blazor WebAssembly you also have the option of a synchronous API, if your use case requires it. You can swap the `ILocalStorageService` for `ISyncLocalStorageService` which allows you to avoid use of `async`/`await`. For either interface, the method names are the same.

With Blazor WebAssembly you also have the option of a synchronous API, if your use case requires it. You can swap the `ISessionStorageService` for `ISyncSessionStorageService` which allows you to avoid use of `async`/`await`. For either interface, the method names are the same.

```c#
@inject MetaFrm.Razor.Storage.Local.ISyncLocalStorageService local
@inject MetaFrm.Razor.Storage.Session.ISyncSessionStorageService session

@code {

    protected override void OnInitialized()
    {
        local.SetItem("name", "John Smith");
        var name = local.GetItem<string>("name");

        session.SetItem("name", "John Smith");
        var name1 = session.GetItem<string>("name");
    }

}
```

## Usage (Blazor Server)

**NOTE:** Due to pre-rendering in Blazor Server you can't perform any JS interop until the `OnAfterRender` lifecycle method.

```c#
@inject MetaFrm.Razor.Storage.Local.ILocalStorageService local
@inject MetaFrm.Razor.Storage.Session.ISessionStorageService session

@code {

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await local.SetItemAsync("name", "John Smith");
        var name = await local.GetItemAsync<string>("name");
        
        await session.SetItemAsync("name", "John Smith");
        var name1 = await session.GetItemAsync<string>("name");
    }

}
```

The APIs available are:

- asynchronous via `ILocalStorageService`:
  - SetItemAsync()
  - SetItemAsStringAsync()
  - GetItemAsync()
  - GetItemAsStringAsync()
  - RemoveItemAsync()
  - ClearAsync()
  - LengthAsync()
  - KeyAsync()
  - ContainKeyAsync()
  
- synchronous via `ISyncLocalStorageService` (Synchronous methods are **only** available in Blazor WebAssembly):
  - SetItem()
  - SetItemAsString()
  - GetItem()
  - GetItemAsString()
  - RemoveItem()
  - Clear()
  - Length()
  - Key()
  - ContainKey()

- asynchronous via `ISessionStorageService`:
  - SetItemAsync()
  - SetItemAsStringAsync()
  - GetItemAsync()
  - GetItemAsStringAsync()
  - RemoveItemAsync()
  - RemoveItemsAsync()
  - ClearAsync()
  - LengthAsync()
  - KeyAsync()
  - KeysAsync()
  - ContainKeyAsync()
  
- synchronous via `ISyncSessionStorageService` (Synchronous methods are **only** available in Blazor WebAssembly):
  - SetItem()
  - SetItemAsString()
  - GetItem()
  - GetItemAsString()
  - RemoveItem()
  - RemovesItem()
  - Clear()
  - Length()
  - Key()
  - Keys()
  - ContainKey()

**Note:** MetaFrm.Razor.Storage LocalStorage methods will handle the serialisation and de-serialisation of the data for you, the exceptions are the `SetItemAsString[Async]` and `GetItemAsString[Async]` methods which will save and return raw string values from local storage.

**Note:** MetaFrm.Razor.Storage SessionStorage methods will handle the serialisation and de-serialisation of the data for you, the exception is the `GetItemAsString[Async]` method which will return the raw string value from session storage.

## Configuring JSON Serializer Options
You can configure the options for the default serializer (System.Text.Json) when calling the `AddStorage` method to register services.

```c#
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("app");

    builder.Services.AddStorage(config =>
        config.JsonSerializerOptions.WriteIndented = true
    );

    await builder.Build().RunAsync();
}
```

## Using a custom JSON serializer
By default, the library uses `System.Text.Json`. If you prefer to use a different JSON library for serialization--or if you want to add some custom logic when serializing or deserializing--you can provide your own serializer which implements the `MetaFrm.Razor.Storage.IJsonSerializer` interface.

To register your own serializer in place of the default one, you can do the following:

```csharp
builder.Services.AddStorage();
builder.Services.Replace(ServiceDescriptor.Scoped<IJsonSerializer, MySerializer>());
```

You can find an example of this in the Blazor Server sample project. The standard serializer has been replaced with a new serializer which uses NewtonsoftJson.