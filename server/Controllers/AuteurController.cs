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
    return Created("test", new { Id = returnValue });
  }
}