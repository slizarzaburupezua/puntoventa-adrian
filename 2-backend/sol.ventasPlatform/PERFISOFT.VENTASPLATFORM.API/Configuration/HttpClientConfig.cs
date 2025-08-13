namespace PERFISOFT.VENTASPLATFORM.API.Configuration
{
    public static class HttpClientConfig
    {
        public static void SetHttpClientConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
        }
    }
}
