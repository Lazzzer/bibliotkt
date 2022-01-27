using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des maisons d'édition
/// </summary>
[ApiController]
public class MaisonEditionController : ControllerBase
{
    private readonly IMaisonEditionService _service;

    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les maisons d'édition
    /// </summary>
    public MaisonEditionController(IMaisonEditionService service)
    {
        _service = service;
    }
    
    /// <remarks>
    /// Retourne toutes les maisons d'éditions
    /// </remarks>
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
    
    /// <remarks>
    /// Retourne une maison d'édition avec ses éditions
    /// </remarks>
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
    
    /// <remarks>
    /// Crée une maison d'édition et retourne son id
    ///
    ///     POST /maisonEdition
    /// 
    ///      {
    ///         "nom": "Glénat",
    ///         "email": "gléna@email.com",
    ///         "rue": "Av. des Sports",
    ///         "noRue": 20,
    ///         "npa": 1401,
    ///         "localite": "Yverdon-les-Bains",
    ///         "pays": "Suisse"
    ///      }
    ///
    /// </remarks>
    [Route("maisonEdition")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateMaisonEdition(MaisonEdition maison)
    {
        return Created("Created", new { Id = _service.Insert(maison) });
    }

    /// <remarks>
    /// Modifie une maison d'édition et retourne 1 si la modification s'est effectuée
    ///
    ///     PUT /maisonEdition
    /// 
    ///      {
    ///         "id": 5,
    ///         "nom": "Glénat",
    ///         "email": "gléna@email.com",
    ///         "rue": "Av. des Sports",
    ///         "noRue": 24,
    ///         "npa": 1401,
    ///         "localite": "Yverdon-les-Bains",
    ///         "pays": "Suisse"
    ///      }
    ///
    /// </remarks>
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
    
    /// <remarks>
    /// Supprime une maison d'édition et retourne 1 si la suppression s'est effectuée
    /// </remarks>
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