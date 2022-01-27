using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des auteurs
/// </summary>
[ApiController]
public class AuteurController : ControllerBase
{
    private readonly IAuteurService _service;

    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les auteurs
    /// </summary>
    public AuteurController(IAuteurService service)
    {
        _service = service;
    }


    /// <remarks>
    /// Retourne tous les auteurs
    /// </remarks>
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
    
    /// <remarks>
    /// Retourne un auteur avec ses livres
    /// </remarks>
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

    /// <remarks>
    /// Crée un auteur et retourne son id
    ///
    ///     POST /auteur
    /// 
    ///     {
    ///        "nom": "Doe",
    ///        "prenom": "John"
    ///     }
    ///
    /// </remarks>
    [Route("auteur")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateAuteur(Auteur auteur)
    {
        return Created("Created", new {Id = _service.Insert(auteur)});
    }


    /// <remarks>
    /// Modifie un auteur et retourne 1 si la modification s'est effectuée
    ///
    ///     PUT /auteur
    /// 
    ///     {
    ///        "id": 7,
    ///        "nom": "Doe",
    ///        "prenom": "Jeanne"
    ///     }
    ///
    /// </remarks>
    [Route("auteur")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateAuteur(Auteur auteur)
    {
        var fetchedAuteur = _service.GetAuteurById(auteur.Id);
        if (fetchedAuteur == null)
            return NotFound();

        return Accepted("Updated", new {AffectedRow = _service.Update(auteur)});
    }

    /// <remarks>
    /// Supprime un auteur et retourne 1 si la suppression s'est effectuée
    /// </remarks>
    [Route("auteur")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteAuteur(int id)
    {
        var fetchedAuteur = _service.GetAuteurById(id);
        if (fetchedAuteur == null)
            return NotFound();

        return Accepted("Deleted", new {AffectedRow = _service.Delete(id)});
    }
}