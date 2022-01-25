using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
public class EditionController : ControllerBase
{
    private readonly IEditionService _service;

    public EditionController(IEditionService service)
    {
        _service = service;
    }
    
    [Route("editions")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEditions()
    {
        var list = _service.GetEditions();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("editions/{issn:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEditionsByIssn(int issn)
    {
        var list = _service.GetEditionsByIssn(issn);
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    [Route("edition/{id:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEditionById(int id)
    {
        var edition = _service.GetEditionById(id);
        if (edition == null)
            return NotFound();

        return Ok(edition);
    }

    [Route("edition")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateEdition(Edition edition)
    {
        return Created("Created", new { Id = _service.Insert(edition) });
    }

    [Route("edition")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateEdition(Edition edition)
    {
        var fetchedEdition = _service.GetEditionById(edition.Id);
        if (fetchedEdition == null)
            return NotFound();

        return Accepted("Updated", new { AffectedRow = _service.Update(edition) });
    }
    
    [Route("edition")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteEdition(int id)
    {
        var fetchedEdition = _service.GetEditionById(id);
        if (fetchedEdition == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(id) });
    }
}