# RESTFulSense WebAssembly Demonstrations

Demonstrates minimum configuration changes to the .Net 6 Blazor WASM templates.

## WASMClient.Client

Minimal 

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

