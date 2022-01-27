using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des membres
/// </summary>
[ApiController]
public class MembreController : ControllerBase
{
    private readonly IMembreService _service;

    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les membres
    /// </summary>
    public MembreController(IMembreService service)
    {
        _service = service;
    }

    /// <remarks>
    /// Retourne tous les membres
    /// </remarks>
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

    /// <remarks>
    /// Retourne un membre avec ses emprunts
    /// </remarks>
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

    /// <remarks>
    /// Crée un membre et retourne son id
    ///
    ///     POST /membre
    /// 
    ///     {
    ///         "nom": "Doe",
    ///         "prenom": "John",
    ///         "rue": "Av. des Sports",
    ///         "noRue": 20,
    ///         "npa": 1401,
    ///         "localite": "Yverdon-les-Bains",
    ///         "dateCreationCompte": "2022-01-27"
    ///     }
    ///
    /// </remarks>
    [Route("membre")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateMembre(Membre membre)
    {
        return Created("Created", new {Id = _service.Insert(membre)});
    }

    /// <remarks>
    /// Modifie un membre et retourne 1 si la modification s'est effectuée
    ///
    ///     PUT /membre
    /// 
    ///     {
    ///         "id": 11,
    ///         "nom": "Doe",
    ///         "prenom": "Jeanne",
    ///         "rue": "Av. des Sports",
    ///         "noRue": 20,
    ///         "npa": 1401,
    ///         "localite": "Yverdon-les-Bains",
    ///         "dateCreationCompte": "2022-01-27"
    ///     }
    ///
    /// </remarks>
    [Route("membre")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateMembre(Membre membre)
    {
        var fetchedMembre = _service.GetMembreById(membre.Id);
        if (fetchedMembre == null)
            return NotFound();

        return Accepted("Updated", new {AffectedRow = _service.Update(membre)});
    }

    /// <remarks>
    /// Supprime un membre et retourne 1 si la suppression s'est effectuée
    /// Il n'est pas possible de supprimer des membres avec des emprunts
    /// </remarks>
    [Route("membre")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteMembre(int id)
    {
        var fetchedMembre = _service.GetMembreById(id);
        if (fetchedMembre == null)
            return NotFound();

        if (fetchedMembre.Emprunts?.Count > 0)
            return BadRequest("Vous ne pouvez pas supprimer des membres avec des emprunts");

        return Accepted("Deleted", new {AffectedRow = _service.Delete(id)});
    }
}