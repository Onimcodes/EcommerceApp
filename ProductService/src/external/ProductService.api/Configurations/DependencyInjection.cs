using Microsoft.EntityFrameworkCore;
using ProductService.infrastructure.Common.DataContext;

namespace ProductService.api.Configurations
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddProductPresentationService (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AppDbConnection")));
            return services;
        }
    }
}
