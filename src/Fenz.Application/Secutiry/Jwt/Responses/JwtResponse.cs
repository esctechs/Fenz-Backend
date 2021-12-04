namespace Fenz.Application.Secutiry.Jwt.Responses;

public class JwtResponse
{
    public string Token { get; }
    public long ExpiresAt { get; }

    public JwtResponse(string token, long expiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }

    public static JwtResponse Create(string token, long expiresAt) =>
        new(token, expiresAt);
}
