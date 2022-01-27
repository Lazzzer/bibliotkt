using Microsoft.AspNetCore.Mvc;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des états d'usure
/// </summary>
[ApiController]
public class EtatUsureController : ControllerBase
{
    private readonly IEtatUsureService _service;

    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les états d'usure
    /// </summary>
    public EtatUsureController(IEtatUsureService service)
    {
        _service = service;
    }
    
    /// <remarks>
    /// Retourne tous les états d'usures
    /// </remarks>
    [Route("etatsUsure")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEtatsUsure()
    {
        var list = _service.GetEtatsUsure();
        if (list.Count == 0)
            return NotFound();

        var stringList = list.Select(c => c.Nom);
        
        return Ok(stringList);
    }
    
    /// <remarks>
    /// Retourne un état d'usure par son nom
    /// </remarks>
    [Route("etatUsure/{nom}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEtatUsureByNom(string nom)
    {
        var etatUsure = _service.GetEtatUsureByNom(nom);
        if (etatUsure == null)
            return NotFound();

        return Ok(etatUsure);
    }
    
    /// <remarks>
    /// Crée un état d'usure et retourne son nom
    /// </remarks>
    [Route("etatUsure")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateEtatUsure(string nom)
    {
        var fetchedEtatUsure = _service.GetEtatUsureByNom(nom);
        
        if (fetchedEtatUsure == null)
            return Created("Created", new { Nom = _service.Insert(nom) });

        return BadRequest("State already exists");
    }

    /// <remarks>
    /// Modifie un état d'usure et retourne 1 si la modification s'est effectuée
    /// </remarks>
    [Route("etatUsure")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateEtatUsure(string nom, string newNom)
    {
        var fetchedEtatUsure = _service.GetEtatUsureByNom(nom);
        if (fetchedEtatUsure == null)
            return NotFound();
            
        return Accepted("Updated", new { AffectedRow = _service.Update(nom, newNom) });
        
    }
    
    /// <remarks>
    /// Supprime un état d'usure et retourne 1 si la suppression s'est effectuée
    /// </remarks>
    [Route("etatUsure")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteEtatUsure(string nom)
    {
        var fetchedEtatUsure = _service.GetEtatUsureByNom(nom);
        if (fetchedEtatUsure == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(nom) });
    }
    
}