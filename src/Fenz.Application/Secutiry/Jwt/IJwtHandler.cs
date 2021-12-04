using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fenz.Application.Secutiry.Jwt.Responses;

namespace Fenz.Application.Secutiry.Jwt;

public interface IJwtHandler
{
    JwtResponse GenerateRsaToken(User user);
}
