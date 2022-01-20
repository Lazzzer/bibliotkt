using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace server.Controllers;

[ApiController]
public class LivreController : ControllerBase
{
    private readonly ILivreService _service;
    
    public LivreController(ILivreService service)
    {
        _service = service;
    }

    [Route("livres")]
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

    [Route("livre/{issn:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivreById(int issn)
    {
        var livre = _service.GetLivreByIssn(issn);
        if (livre == null)
            return NotFound();

        return Ok(livre);
    }
    
    [Route("livresByTitle")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivresByTitle(string titre)
    {
        var list = _service.GetLivresByTitle(titre);
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("livresByFilters")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivreWithFilters(string? nomAuteur, Langue? langue, [FromQuery] string[] nomCategories)
    {
        var list = _service.GetLivresByFilters(nomAuteur, langue, nomCategories, true);
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("/livre/recommandations")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivreWithFilters(int issn, string? nomAuteur, [FromQuery] string[] nomCategories)
    {
        var list = _service.GetLivresByFilters(nomAuteur, null, nomCategories, false).ToList();

        list.RemoveAll(l => l.Issn == issn);
        
        
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
}