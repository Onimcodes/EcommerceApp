using IdentityService.infrastructure.Authentication;
using IdentityService.infrastructure.Common.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityService.api.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityPresentationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AppDbConnection")));

            // JWT Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtOptions:Issuer"],
                    ValidAudience = configuration["JwtOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Secret"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero

                };
                x.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        // Override default behavior, suppress the WWW-Authenticate header
                        context.HandleResponse();

                        // Customize the response sent back
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var result = new
                        {
                            ResponseCode = context.Response.StatusCode,
                            ResponseMessage = "Unauthorized. Token is missing, expired, or invalid."
                        };

                        return context.Response.WriteAsJsonAsync(result);
                    }
                };
            });
            services.AddAuthorization();
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            return services;
        }
    }
}
