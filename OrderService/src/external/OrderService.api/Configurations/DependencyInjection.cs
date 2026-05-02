using Microsoft.EntityFrameworkCore;
using OrderService.infrastructure.Common.DataContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrderService.api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrderPresentationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AppDbConnection")));
            return services;
        }
    }
}
