using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
public class EmpruntController : ControllerBase
{
    private readonly IEmpruntService _service;

    public EmpruntController(IEmpruntService service)
    {
        _service = service;
    }
    
    [Route("emprunts")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmprunts(bool retard = false)
    {
        var list = retard ? _service.GetEmpruntsEnRetard() : _service.GetEmprunts();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("emprunt/{id:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmpruntById(int id)
    {
        var emprunt = _service.GetEmpruntById(id);
        if (emprunt == null)
            return NotFound();

        return Ok(emprunt);
    }
    
    [Route("emprunts/membre/{idMembre:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmpruntsByIdMembre(int idMembre, bool retard = false)
    {

        var list = retard ? _service.GetEmpruntsEnRetardByIdMembre(idMembre) : _service.GetEmpruntsByIdMembre(idMembre);
        
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("emprunts/exemplaire/{idExemplaire:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmpruntsByIdExemplaire(int idExemplaire, bool retard = false)
    {

        var list = retard ? _service.GetEmpruntsEnRetardByIdExemplaire(idExemplaire) : _service.GetEmpruntsByIdExemplaire(idExemplaire);
        
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("emprunts/actuel")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmpruntsByidExemplaire(bool retard = false)
    {
        var list = retard ? _service.GetEmpruntsActuelsEnRetard() : _service.GetEmpruntsActuels();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("emprunt")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateEmprunt(Emprunt emprunt)
    {
        return Created("Created", new { Id = _service.Insert(emprunt) });
    }

    [Route("emprunt")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateEmprunt(Emprunt emprunt)
    {
        var fetchedEmprunt = _service.GetEmpruntById(emprunt.Id);
        if (fetchedEmprunt == null)
            return NotFound();

        return Accepted("Updated", new { AffectedRow = _service.Update(emprunt) });
    }
    
    [Route("emprunt")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteEmprunt(int id)
    {
        var fetchedEmprunt = _service.GetEmpruntById(id);
        if (fetchedEmprunt == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(id) });
    }
}