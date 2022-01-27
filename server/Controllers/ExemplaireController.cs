using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
public class ExemplaireController : ControllerBase
{
    private readonly IExemplaireService _service;

    public ExemplaireController(IExemplaireService service)
    {
        _service = service;
    }
    
    [Route("exemplaires")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetExemplaires()
    {
        var list = _service.GetExemplaires();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }

    [Route("exemplaires/{issn:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetExemplaireByIssn(int issn)
    {
        var list = _service.GetExemplairesByIssn(issn);
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("exemplaire/{id:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetExemplaireById(int id)
    {
        var exemplaire = _service.GetExemplaireById(id);
        if (exemplaire == null)
            return NotFound();

        return Ok(exemplaire);
    }
    
    [Route("nbExemplaires/{issn:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetNbExemplaires(int issn)
    {
        var exemplaire = _service.GetNbExemplaires(issn);

        return Ok(exemplaire);
    }
    
    [Route("exemplaire")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateExemplaire(Exemplaire exemplaire)
    {
        return Created("Created", new { Id = _service.Insert(exemplaire) });
    }

    [Route("exemplaire")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateExemplaire(Exemplaire exemplaire)
    {
        var fetchedExemplaire = _service.GetExemplaireById(exemplaire.Id);
        if (fetchedExemplaire == null)
            return NotFound();

        return Accepted("Updated", new { AffectedRow = _service.Update(exemplaire) });
    }
    
    [Route("exemplaire")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteExemplaire(int id)
    {
        var fetchedExemplaire = _service.GetExemplaireById(id);
        if (fetchedExemplaire == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(id) });
    }
}