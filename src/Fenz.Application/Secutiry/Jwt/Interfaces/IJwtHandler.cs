using System.Threading.Tasks;
using Fenz.Application.Dtos;

namespace Fenz.Application.Secutiry.Jwt.Interfaces;

public interface IJwtHandler
{
    Task<AuthResult> GenerateRsaToken(User user);
}
