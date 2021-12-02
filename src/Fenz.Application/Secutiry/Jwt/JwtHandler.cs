using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Fenz.Application.Secutiry.Jwt.Responses;
using Microsoft.IdentityModel.Tokens;

namespace Fenz.Application.Secutiry.Jwt;

public static class JwtHandler
{
    private static readonly string Secret = "fedaf7d8863b48e197b9287d492b708e";
    public static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
        //return string.Empty;
    }

    public static JwtResponse GenerateRsaToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler
        {
            SetDefaultTimesOnTokenCreation = false
        };
        var exampleKey = "example";
        var privateKey = exampleKey.ToByteArray();

        using RSA rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(privateKey, out _);

        var signinCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
        {
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };

        var unixTimeSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.Name, "userName"),
                 new Claim(ClaimTypes.Role, "userRole")
            }),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = signinCredentials
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);
        return JwtResponse.Create(token, unixTimeSeconds);
    }
}

public static class TypeConverterExtension
{
    public static byte[] ToByteArray(this string value) =>
        Convert.FromBase64String(value);
}
