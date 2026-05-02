using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProductApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });


            return services;
        }
    }
}
