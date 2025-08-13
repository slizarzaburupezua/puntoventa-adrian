using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.CROSSCUTTING.MapperExtensions;

namespace PERFISOFT.VENTASPLATFORM.API.Configuration
{
    public static class MapperConfig
    {
        public static void SetAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MapperExtensions));
        }
    }
}
