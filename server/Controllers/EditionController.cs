using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des éditions
/// </summary>
[ApiController]
public class EditionController : ControllerBase
{
    private readonly IEditionService _service;

    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les édition
    /// </summary>
    public EditionController(IEditionService service)
    {
        _service = service;
    }

    /// <remarks>
    /// Retourne toutes les éditions
    /// </remarks>
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

    /// <remarks>
    /// Retourne tous les éditions pour un ISSN donné
    /// </remarks>
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

    /// <remarks>
    /// Retourne une édition avec ses exemplaires
    /// </remarks>
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

    /// <remarks>
    /// Crée une édition et retourne son id
    ///
    ///     POST /edition
    ///
    ///     {
    ///        "issn": 12345678,
    ///        "idMaison": 1,
    ///        "type": 2,
    ///        "langue": 3
    ///     }
    /// 
    /// </remarks>
    [Route("edition")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateEdition(Edition edition)
    {
        return Created("Created", new {Id = _service.Insert(edition)});
    }

    /// <remarks>
    /// Modifie une édition et retourne 1 si la modification s'est effectuée
    ///
    ///     PUT /edition
    /// 
    ///     {
    ///        "id": 73,
    ///        "issn": 12345678,
    ///        "idMaison": 1,
    ///        "type": 3,
    ///        "langue": 4
    ///     }
    ///
    /// </remarks>
    [Route("edition")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateEdition(Edition edition)
    {
        var fetchedEdition = _service.GetEditionById(edition.Id);
        if (fetchedEdition == null)
            return NotFound();

        return Accepted("Updated", new {AffectedRow = _service.Update(edition)});
    }

    /// <remarks>
    /// Supprime une édition et retourne 1 si la suppression s'est effectuée
    /// </remarks>
    [Route("edition")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteEdition(int id)
    {
        var fetchedEdition = _service.GetEditionById(id);
        if (fetchedEdition == null)
            return NotFound();

        return Accepted("Deleted", new {AffectedRow = _service.Delete(id)});
    }
}