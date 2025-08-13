using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;

namespace PERFISOFT.VENTASPLATFORM.API.Configuration
{
    public static class DBConnection
    {
        public static void SetDBConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VentasPlatformContext>(options => options.UseSqlServer(configuration.GetConnectionString("VentasConnection")));
        }
    }
}
