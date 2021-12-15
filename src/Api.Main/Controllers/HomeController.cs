using System;
using System.Threading.Tasks;
using Fenz.Application;
using Fenz.Application.Dtos;
using Fenz.Application.Secutiry.Jwt.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Main.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{

    private readonly IAccessManager _accessManager;

    public HomeController(IAccessManager accessManager)
    {
        _accessManager = accessManager;
    }

    //Use grant type for signin and refresh token
    [HttpPost]
    [Route("login")]
    public async Task<AuthResult> Authenticate([FromBody] User model)
    {
        var user = UserRepository.Get(model.Username, model.Password);

        //var token = await _jwtHandler.GenerateRsaToken(user);
        throw new NotImplementedException();
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

    [HttpGet]
    [Route("finance")]
    [Authorize(Roles = "finance")]
    public string Finance() => "Finances";
}
