using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Fenz.Application.Dtos;
using Fenz.Application.Secutiry.Jwt.Interfaces;
using Fenz.Application.Secutiry.Jwt.Models;
using Microsoft.IdentityModel.Tokens;

namespace Fenz.Application.Secutiry.Jwt;

public class JwtHandler : IJwtHandler
{
    private readonly JwtSettings _jwtSettings;
    public JwtHandler(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public async Task<AuthResult> GenerateRsaToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler
        {
            SetDefaultTimesOnTokenCreation = false
        };

        var pkPem = await File.ReadAllTextAsync(_jwtSettings.Cert);

        var formatedPrivateKey = RemoveHeaderAndFooterPemText(pkPem, _jwtSettings);
        var privateKey = formatedPrivateKey.ToByteArray();

        using RSA rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(privateKey, out _);

        var signinCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
        {
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };

        var unixTimeSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("name", user.Username),
                 new Claim("role", user.Role),
                 new Claim("aud", _jwtSettings.Audience),
                 new Claim("iss", _jwtSettings.Issuer),
                 //new Claim(ClaimTypes.Email, "userEmail"),
            }),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = signinCredentials
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);

        return AuthResult.Create(true, token);
    }

    public static string RemoveHeaderAndFooterPemText(string pemKey, IJwtSettings jwtSettings)
    {
        var privateKey = pemKey.Replace(jwtSettings.PemHeader, string.Empty)
                               .Replace(jwtSettings.PemFooter, string.Empty);
        return privateKey;
    }
}

public static class TypeConverterExtension
{
    public static byte[] ToByteArray(this string value) =>
        Convert.FromBase64String(value);
}
