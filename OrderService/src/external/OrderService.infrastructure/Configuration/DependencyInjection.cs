using Microsoft.Extensions.DependencyInjection;
using OrderService.application.Interfaces.Persistence;
using OrderService.infrastructure.Persistence;

namespace OrderService.infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrderInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
