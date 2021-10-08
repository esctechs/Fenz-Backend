using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Api.Main.Config.ServicesCollections.Authorization
{
    public static class IdentityServerServiceCollection
    {
        public static IServiceCollection AddIdentityServer4(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(opts =>
                {
                    opts.JwtBackChannelHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    };
                    opts.RequireHttpsMetadata = false;
                    opts.Authority = configuration["IdentityServer4:Authority"];
                    opts.ApiName = configuration["IdentityServer4:Audience"];
                });

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("fenz-services", builder =>
                {
                    builder.RequireScope("fenz.api");
                });
            });

            return services;
        }
    }
}
