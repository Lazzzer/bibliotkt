using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
public class EmployeController : ControllerBase
{
    private readonly IEmployeService _service;

    public EmployeController(IEmployeService service)
    {
        _service = service;
    }
    
    [Route("employe/login")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult Login(string login, string password)
    {
        var employe = _service.GetEmployeByLogin(login);
        
        if (employe == null)
            return NotFound();

        if (employe.Password != password)
            return Unauthorized();

        return Ok("Logged as " + login);
    }
}