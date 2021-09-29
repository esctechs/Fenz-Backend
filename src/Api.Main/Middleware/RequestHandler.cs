using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sentry;

namespace Api.Main.Middleware
{
    public class RequestHandler : IRequestHandler
    {
        //Initial implementation for get exception from all environments
        readonly HttpException _httpException;

        public RequestHandler(IOptions<HttpException> httpException) => _httpException = httpException.Value;
        public Task HandleRequests(HttpContext context, Exception exception)
        {
            SentrySdk.CaptureException(exception);

            return context.Response.WriteAsync(new HttpException()
            {
                StatusCode = _httpException.StatusCode,
                Message = _httpException.Message
            }.ToString());
        }
    }
}