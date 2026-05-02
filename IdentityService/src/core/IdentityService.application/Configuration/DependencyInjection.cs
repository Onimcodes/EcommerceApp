using IdentityService.application.Common.PipleLineValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineValidationBehaviour<,>));


            return services;
        }
    }
}
