using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RESTFulSense.WebAssembly.Clients;
using RESTFulSense.WebAssembly.Services;
using WASMFactoryClient.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRESTFulApiClient(
    name: "WASMFactoryClient.ServerAPI",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
        .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped<IRESTFulApiFactoryClient>(
    sp => sp.GetRequiredService<IRESTFulApiClientFactory>().CreateClient(name: "WASMFactoryClient.ServerAPI"));

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
