using Fenz.Application.Secutiry.Jwt.Interfaces;

namespace Fenz.Application.Secutiry.Jwt.Models;

public class JwtSettings : IJwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string PemHeader { get; set; }
    public string PemFooter { get; set; }
    public string Cert { get; set; }
    public string PublicKey { get; set; }

    public JwtSettings() { }

    public static JwtSettings Create() => new();
}
