using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api.Main.Config.ServicesCollections.Authorization;

public static class AsymmetricAuth
{
    public static IServiceCollection AddAsymmetricAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<RsaSecurityKey>(provider =>
        {
            RSA rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(configuration["JwtSettings:PublicKey"]), out _);
            return new RsaSecurityKey(rsa);
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireClaim("role", "finance"));
        });

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(opt =>
            {
                opt.Audience = configuration["JwtSettings:Audience"];
                opt.ClaimsIssuer = configuration["JwtSettings:Issuer"];
                var rsa = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();
                opt.IncludeErrorDetails = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = rsa,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateIssuer = true,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
            });
        return services;
    }
}
