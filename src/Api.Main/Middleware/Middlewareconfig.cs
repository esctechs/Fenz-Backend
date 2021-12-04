using Microsoft.AspNetCore.Builder;

namespace Api.Main.Middleware;

public static class Middlewareconfig
{
    public static void AddMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<Middleware>();
}
