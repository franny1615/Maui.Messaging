using Messaging.Api.Repositories.LoginRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Api.Controllers;

[ApiController]
public class LoginController(ILoginRepository loginRepo)
{
    [Route("login")]
    [HttpGet]
    [AllowAnonymous]
    public object Login()
    {
        return new { Token = loginRepo.GetJwtToken() };
    }
}
