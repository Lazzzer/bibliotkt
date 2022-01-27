using Microsoft.AspNetCore.Mvc;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des catégories de livres
/// </summary>
[ApiController]
public class CategorieController : ControllerBase
{
    private readonly ICategorieService _service;

    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les catégories
    /// </summary>
    public CategorieController(ICategorieService service)
    {
        _service = service;
    }

    /// <remarks>
    /// Retourne toutes les catégories
    /// </remarks>
    [Route("categories")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetCategories()
    {
        var list = _service.GetCategories();
        if (list.Count == 0)
            return NotFound();

        var stringList = list.Select(c => c.Nom);

        return Ok(stringList);
    }

    /// <remarks>
    /// Retourne une catégorie
    /// </remarks>
    [Route("categorie/{nom}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetCategorieByNom(string nom)
    {
        var categorie = _service.GetCategorieByNom(nom);
        if (categorie == null)
            return NotFound();

        return Ok(categorie);
    }

    /// <remarks>
    /// Crée une catégorie et retourne son nom
    /// </remarks>
    [Route("categorie")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateCategorie(string nom)
    {
        var fetchedCategorie = _service.GetCategorieByNom(nom);

        if (fetchedCategorie == null)
            return Created("Created", new {Id = _service.Insert(nom)});

        return BadRequest("Categorie already exists");
    }

    /// <remarks>
    /// Modifie une catégorie et retourne 1 si la modification s'est effectuée
    /// </remarks>
    [Route("categorie")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateCategorie(string nom, string newNom)
    {
        var fetchedCategorie = _service.GetCategorieByNom(nom);
        if (fetchedCategorie == null)
            return NotFound();

        return Accepted("Updated", new {AffectedRow = _service.Update(nom, newNom)});
    }

    /// <remarks>
    /// Supprime une catégorie et retourne 1 si la suppression s'est effectuée
    /// </remarks>
    [Route("categorie")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteCategorie(string nom)
    {
        var fetchedCategorie = _service.GetCategorieByNom(nom);
        if (fetchedCategorie == null)
            return NotFound();

        return Accepted("Deleted", new {AffectedRow = _service.Delete(nom)});
    }
}