using Microsoft.AspNetCore.Mvc;
using server.Services;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
public class EtatUsureController : ControllerBase
{
    private readonly IEtatUsureService _service;

    public EtatUsureController(IEtatUsureService service)
    {
        _service = service;
    }
    
    [Route("etatsUsure")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEtatsUsure()
    {
        var list = _service.GetEtatsUsure();
        if (list.Count == 0)
            return NotFound();

        var stringList = list.Select(c => c.Nom);
        
        return Ok(stringList);
    }
    
    [Route("etatUsure/{nom}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEtatUsureByNom(string nom)
    {
        var etatUsure = _service.GetEtatUsureByNom(nom);
        if (etatUsure == null)
            return NotFound();

        return Ok(etatUsure);
    }
    
    [Route("etatUsure")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateEtatUsure(string nom)
    {
        var fetchedEtatUsure = _service.GetEtatUsureByNom(nom);
        
        if (fetchedEtatUsure == null)
            return Created("Created", new { Issn = _service.Insert(nom) });

        return BadRequest("State already exists");
    }

    [Route("etatUsure")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateEtatUsure(string nom, string newNom)
    {
        var fetchedEtatUsure = _service.GetEtatUsureByNom(nom);
        if (fetchedEtatUsure == null)
            return NotFound();
            
        return Accepted("Updated", new { AffectedRow = _service.Update(nom, newNom) });
        
    }
    
    [Route("etatUsure")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteEtatUsure(string nom)
    {
        var fetchedEtatUsure = _service.GetEtatUsureByNom(nom);
        if (fetchedEtatUsure == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(nom) });
    }
    
}