using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des exemplaires
/// </summary>
[ApiController]
public class ExemplaireController : ControllerBase
{
    private readonly IExemplaireService _service;

    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les exemplaires
    /// </summary>
    public ExemplaireController(IExemplaireService service)
    {
        _service = service;
    }
    
    /// <remarks>
    /// Retourne tous les exemplaires
    /// </remarks>
    [Route("exemplaires")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetExemplaires()
    {
        var list = _service.GetExemplaires();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }

    /// <remarks>
    /// Retourne tous les exemplaires par l'issn d'un livre
    /// </remarks>
    [Route("exemplaires/{issn:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetExemplaireByIssn(int issn)
    {
        var list = _service.GetExemplairesByIssn(issn);
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    /// <remarks>
    /// Retourne un exemplaire par son id
    /// </remarks>
    [Route("exemplaire/{id:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetExemplaireById(int id)
    {
        var exemplaire = _service.GetExemplaireById(id);
        if (exemplaire == null)
            return NotFound();

        return Ok(exemplaire);
    }
    
    /// <remarks>
    /// Retourne le nombre d'exemplaires pour un livre
    /// </remarks>
    [Route("nbExemplaires/{issn:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetNbExemplaires(int issn)
    {
        var exemplaire = _service.GetNbExemplaires(issn);

        return Ok(exemplaire);
    }
    
    /// <remarks>
    /// Crée un exemplaire et retourne son id
    ///
    ///     POST /exemplaire
    /// 
    ///     {
    ///        "issnLivre": "12345678",
    ///        "idEdition": 1
    ///     }
    ///
    /// </remarks>
    [Route("exemplaire")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateExemplaire(Exemplaire exemplaire)
    {
        return Created("Created", new { Id = _service.Insert(exemplaire) });
    }

    /// <remarks>
    /// Supprime un exemplaire et retourne 1 si la suppression s'est effectuée
    /// </remarks>
    [Route("exemplaire")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteExemplaire(int id)
    {
        var fetchedExemplaire = _service.GetExemplaireById(id);
        if (fetchedExemplaire == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(id) });
    }
}