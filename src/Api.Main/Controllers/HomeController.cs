using System.Threading.Tasks;
using Fenz.Application;
using Fenz.Application.Secutiry.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
    {
        var user = UserRepository.Get(model.Username, model.Password);

        if (user == null)
            return NotFound(new { message = "Invalid user or password" });

        var token = JwtHandler.GenerateToken(user);
        user.Password = string.Empty;

        return Task.FromResult(new
        {
            user = user,
            token = token,
        });
    }

    [HttpGet]
    [Route("anonymous")]
    [AllowAnonymous]
    public string Anonymous() => "Anônimo";

    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public string Authenticated()
    {
        return string.Format("Autenticado - {0}", User.Identity.Name);
    }

    [HttpGet]
    [Route("employee")]
    [Authorize(Roles = "employee,manager")]
    public string Employee() => "Funcionário";

    [HttpGet]
    [Route("manager")]
    [Authorize(Roles = "manager")]
    public string Manager() => "Gerente";
}
