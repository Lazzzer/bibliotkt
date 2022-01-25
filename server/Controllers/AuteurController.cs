using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;
using server.Services.Interfaces;

namespace server.Controllers;
[ApiController]
public class AuteurController : ControllerBase
{
  private readonly IAuteurService _service;

  public AuteurController(IAuteurService service)
  {
    _service = service;
  }

  [Route("auteurs")]
  [HttpGet]
  [Produces("application/json")]
  [Consumes("application/json")]
  public ActionResult GetAuteurs()
  {
    var list = _service.GetAuteurs();
    if (list.Count == 0)
      return NotFound();

    return Ok(list);
  }

  [Route("auteur/{id:int}")]
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

  [Route("auteur")]
  [HttpPost]
  [Produces("application/json")]
  [Consumes("application/json")]
  public ActionResult CreateAuteur(Auteur auteur)
  {
    return Created("Created", new { Id = _service.Insert(auteur) });
  }

  [Route("auteur")]
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
  
  [Route("auteur")]
  [HttpDelete]
  [Produces("application/json")]
  [Consumes("application/json")]
  public ActionResult DeleteAuteur(int id)
  {
    var fetchedAuteur = _service.GetAuteurById(id);
    if (fetchedAuteur == null)
      return NotFound();

    return Accepted("Deleted", new { AffectedRow = _service.Delete(id) });
  }
}