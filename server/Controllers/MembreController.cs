using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
public class MembreController : ControllerBase
{
    private readonly IMembreService _service;

    public MembreController(IMembreService service)
    {
        _service = service;
    }
    
    [Route("membres")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetMembres()
    {
        var list = _service.GetMembres();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("membre/{id:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetMembreById(int id)
    {
        var membre = _service.GetMembreById(id);
        if (membre == null)
            return NotFound();

        return Ok(membre);
    }
    
    [Route("membre")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateMembre(Membre membre)
    {
        return Created("Created", new { Id = _service.Insert(membre) });
    }

    [Route("membre")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateMembre(Membre membre)
    {
        var fetchedMembre = _service.GetMembreById(membre.Id);
        if (fetchedMembre == null)
            return NotFound();

        return Accepted("Updated", new { AffectedRow = _service.Update(membre) });
    }
    
    [Route("membre")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteMembre(int id)
    {
        var fetchedMembre = _service.GetMembreById(id);
        if (fetchedMembre == null)
            return NotFound();

        if (fetchedMembre.Emprunts.Count > 0)
            return BadRequest("Vous ne pouvez pas supprimer des membres avec des emprunts");
        
        return Accepted("Deleted", new { AffectedRow = _service.Delete(id) });
    }
}