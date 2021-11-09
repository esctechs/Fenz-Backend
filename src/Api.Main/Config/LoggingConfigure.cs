using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Main.Config;

public static class LoggingConfigure
{
    public static IServiceCollection AddLoggingConfigure(this IServiceCollection services) =>
        services.AddLogging(log =>
                       log.AddSimpleConsole(c =>
                       {
                           c.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
                       })
                        .AddEventLog());
}