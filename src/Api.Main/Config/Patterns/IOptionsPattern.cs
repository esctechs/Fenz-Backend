using Api.Main.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Main.Config.Patterns;

public static class IOptionsPattern
{
    public static IServiceCollection AddIOptionsPattern(this IServiceCollection services, IConfiguration config) =>
        services.Configure<HttpException>(config.GetSection("HttpException"));
}
