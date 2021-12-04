using Api.Main.Middleware;
using Fenz.Application.Secutiry.Jwt;
using Fenz.Application.Secutiry.Jwt.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Main.Config.ServicesCollections.Fenz;

public static class FenzServices
{
    public static IServiceCollection AddFenzServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region[Jwt]
        var jwtSettings = JwtSettings.Create();
        configuration.GetSection("JwtSettings").Bind(jwtSettings);

        services.AddScoped<IJwtHandler, JwtHandler>();
        services.AddSingleton(jwtSettings);
        #endregion

        #region[Sentry]
        services.AddSingleton<IRequestHandler, RequestHandler>();
        #endregion
        return services;
    }
}
