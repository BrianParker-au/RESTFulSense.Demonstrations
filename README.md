# RESTFulSense WebAssembly Demonstrations

Demonstrates minimum configuration changes to the .Net 6 Blazor WASM templates.

## WASM Client

```
Install-Package RESTFulSense.WebAssembly -Version 0.9.0
```

`program.cs`

Update this line:
```
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
```
to:
```
using RESTFulSense.WebAssembly.Clients;
...

builder.Services.AddScoped(sp => new RESTFulApiClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
```

`FetchData.razor`
```
@using RESTFulSense.WebAssembly.Clients
@inject RESTFulApiClient Http

...
@code {
    ...
        forecasts = await Http.GetContentAsync<WeatherForecast[]>("WeatherForecast");
    ...
}

```

Alternately you can inject by Interface to allow Mocking.

### Injecting Interface (Unit testing support)

`program.cs`
```
builder.Services.AddScoped<IRESTFulApiClient>(
    serviceProvider => new RESTFulApiClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
```

`FetchData.razor`
```
@using RESTFulSense.WebAssembly.Clients
@inject IRESTFulApiClient Http

...
@code {
    ...
        forecasts = await Http.GetContentAsync<WeatherForecast[]>("WeatherForecast");
    ...
}
```

## WASM Factory Client

```
Install-Package RESTFulSense.WebAssembly -Version 0.9.0
```

`program.cs`

Update this line:
```
builder.Services.AddHttpClient("WASMFactoryClient.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WASMFactoryClient.ServerAPI"));
```
to:
```
using RESTFulSense.WebAssembly.Clients;
using RESTFulSense.WebAssembly.Services;
...

builder.Services.AddRESTFulApiClient(
    name: "WASMFactoryClient.ServerAPI",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
        .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped<IRESTFulApiFactoryClient>(
    sp => sp.GetRequiredService<IRESTFulApiClientFactory>().CreateClient(name: "WASMFactoryClient.ServerAPI"));

```

`FetchData.razor`
```
@using RESTFulSense.WebAssembly.Clients
@inject IRESTFulApiFactoryClient Http

...
@code {
    ...
        forecasts = await Http.GetContentAsync<WeatherForecast[]>("WeatherForecast");
    ...
}

```
