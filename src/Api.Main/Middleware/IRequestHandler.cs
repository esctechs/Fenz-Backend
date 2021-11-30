using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Api.Main.Middleware;

public interface IRequestHandler
{
    Task HandleRequests(HttpContext context, Exception exception);
}
