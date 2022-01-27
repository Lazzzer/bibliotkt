using Microsoft.AspNetCore.Mvc;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des employés
/// </summary>
[ApiController]
public class EmployeController : ControllerBase
{
    private readonly IEmployeService _service;

    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les employés
    /// </summary>
    public EmployeController(IEmployeService service)
    {
        _service = service;
    }
    
    /// <remarks>
    /// Effectue une tentative de connexion. Retourne un code 200 si la tentative a réussi.
    /// </remarks>
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