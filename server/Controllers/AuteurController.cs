using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;

namespace server.Controllers;
[ApiController]
[Route("auteur")]
public class AuteurController : ControllerBase
{
  private readonly IAuteurService _service;

  public AuteurController(IAuteurService service)
  {
    _service = service;
  }

  [HttpGet]
  [Produces("application/json")]
  [Consumes("application/json")]
  public ActionResult GetAuteurs(int? limit, int? offset)
  {
    var list = _service.GetAuteurs(limit, offset);
    if (list.Count == 0)
      return NotFound();

    return Ok(list);
  }

  [Route("{id:int}")]
  [HttpGet]
  [Produces("application/json")]
  [Consumes("application/json")]
  public ActionResult GetAuteurById(int id)
  {
    var auteur = _service.GetAuteurByIdWithLivres(id);
    if (auteur == null)
      return NotFound();

    return Ok(auteur);
  }

  [HttpPost]
  [Produces("application/json")]
  [Consumes("application/json")]
  public ActionResult CreateAuteur(Auteur auteur)
  {
    var returnValue = _service.Insert(auteur);
    return Created("Created", new { Id = _service.Insert(auteur) });
  }

  [HttpPut]
  [Produces("application/json")]
  [Consumes("application/json")]
  public ActionResult UpdateAuteur(Auteur auteur)
  {
    var fetchedAuteur = _service.GetAuteurById(auteur.Id);
    if (fetchedAuteur == null)
      return NotFound();

    return Accepted("Updated", new { AffectedRow = _service.Update(auteur) });
  }
  [HttpDelete]
  [Produces("application/json")]
  [Consumes("application/json")]
  public ActionResult DeleteAuteur(Auteur auteur)
  {
    var fetchedAuteur = _service.GetAuteurById(auteur.Id);
    if (fetchedAuteur == null)
      return NotFound();

    return Accepted("Deleted", new { AffectedRow = _service.Delete(auteur) });
  }
}