using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace server.Controllers;

[ApiController]
public class MaisonEditionController : ControllerBase
{
    private readonly IMaisonEditionService _service;

    public MaisonEditionController(IMaisonEditionService service)
    {
        _service = service;
    }
    
    [Route("maisonsEdition")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetMaisonsEdition()
    {
        var list = _service.GetMaisons();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("maisonEdition/{id:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetMaisonEditionById(int id)
    {
        var maisonEdition = _service.GetMaisonById(id);
        if (maisonEdition == null)
            return NotFound();

        return Ok(maisonEdition);
    }
    
    [Route("maisonEdition")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateMaisonEdition(MaisonEdition maison)
    {
        return Created("Created", new { Id = _service.Insert(maison) });
    }

    [Route("maisonEdition")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateMaisonEdition(MaisonEdition maison)
    {
        var fetchedMaison = _service.GetMaisonById(maison.Id);
        if (fetchedMaison == null)
            return NotFound();

        return Accepted("Updated", new { AffectedRow = _service.Update(maison) });
    }
    
    [Route("maisonEdition")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteMaisonEdition(int id)
    {
        var fetchedMaison = _service.GetMaisonById(id);
        if (fetchedMaison == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(id) });
    }
}