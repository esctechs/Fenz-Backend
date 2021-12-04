using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Api.Main.Middleware;

public class Middleware
{
    readonly RequestDelegate _next;
    public Middleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, IRequestHandler middleware)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await middleware.HandleRequests(context, ex);
        }
    }
}
