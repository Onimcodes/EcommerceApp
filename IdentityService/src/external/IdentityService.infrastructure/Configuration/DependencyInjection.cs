using Microsoft.Extensions.DependencyInjection;
using IdentityService.application.Interfaces.Persistence;
using IdentityService.infrastructure.Persistence;
using IdentityService.application.Interfaces;
using IdentityService.infrastructure.Authentication;

namespace IdentityService.infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthTokenService, AuthTokenService> ();
            return services;
        }
    }
}
