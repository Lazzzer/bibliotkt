using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;
using server.Services.Interfaces;

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
    public ActionResult GetLivreByIssn(int issn)
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
    public ActionResult GetRecommandation(int issn, string? nomAuteur, [FromQuery] string[] nomCategories)
    {
        var list = _service.GetLivresByFilters(nomAuteur, null, nomCategories, false).ToList();

        list.RemoveAll(l => l.Issn == issn);
        
        
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("livre")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateLivre(Livre livre)
    {
        return Created("Created", new { Issn = _service.Insert(livre) });
    }

    [Route("livre")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateLivre(Livre livre)
    {
        var fetchedLivre = _service.GetLivreByIssn(livre.Issn);
        if (fetchedLivre == null)
            return NotFound();

        return Accepted("Updated", new { AffectedRow = _service.Update(livre) });
    }
    
    [Route("livre")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteLivre(int issn)
    {
        var fetchedLivre = _service.GetLivreByIssn(issn);
        if (fetchedLivre == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(issn) });
    }
}