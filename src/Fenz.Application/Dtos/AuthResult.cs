using System;

namespace Fenz.Application.Dtos;

public class AuthResult
{
    public bool Authenticated { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }

    private AuthResult(bool authenticated, string token)
    {
        Authenticated = authenticated;
        Token = token;
        RefreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);
    }

    public static AuthResult Create(bool authenticated, string token) =>
        new(authenticated, token);
}
