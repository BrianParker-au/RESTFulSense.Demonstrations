using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RESTFulSense.WebAssembly.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddRESTFulApiClient(
            this IServiceCollection services,
            string name,
            Action<HttpClient> configureHttpClient)
        {
            services.TryAddSingleton<IRESTFulApiClientFactory, RESTFulApiClientFactory>();

            return services.AddHttpClient(name, configureHttpClient);
        }
    }
}

