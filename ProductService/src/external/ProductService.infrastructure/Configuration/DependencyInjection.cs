using Microsoft.Extensions.DependencyInjection;
using ProductService.application.Interfaces.Persistence;
using ProductService.infrastructure.Persistence;

namespace ProductService.infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProductInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;

        }
    }
}
