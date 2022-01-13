using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("livre")]
public class LivreController : ControllerBase
{
    private readonly ILivreService _service;
    
    public LivreController(ILivreService service)
    {
        _service = service;
    }

    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivres()
    {
        var list = _service.GetLivres();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }

    [Route("{issn:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivreById(int issn)
    {
        var livre = _service.GetLivreByIssnWithAuteursEtCategories(issn);
        if (livre == null)
            return NotFound();

        return Ok(livre);
    }
}