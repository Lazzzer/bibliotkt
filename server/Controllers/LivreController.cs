using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des livres
/// </summary>
[ApiController]
public class LivreController : ControllerBase
{
    private readonly ILivreService _service;
    
    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les livres
    /// </summary>
    public LivreController(ILivreService service)
    {
        _service = service;
    }

    /// <remarks>
    /// Retourne tous les livres
    /// </remarks>
    [Route("livres")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivres()
    {
        var list = _service.GetLivres();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }

    /// <remarks>
    /// Retourne un livre avec ses auteurs, ses catégories et ses éditions via son Issn
    /// </remarks>
    [Route("livre/{issn:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivreByIssn(int issn)
    {
        var livre = _service.GetLivreByIssn(issn);
        if (livre == null)
            return NotFound();

        return Ok(livre);
    }
    
    /// <remarks>
    /// Retourne tous les livres correspondant au titre transmis
    /// </remarks>
    [Route("livresByTitle")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivresByTitle(string titre)
    {
        var list = _service.GetLivresByTitle(titre);
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    /// <remarks>
    /// Retourne tous les livres correspondant aux filtres transmis.
    /// Possibilité de filtrer par nom d'auteur, par langue et par catégories
    /// </remarks>
    [Route("livresByFilters")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetLivreWithFilters(string? nomAuteur, Langue? langue, [FromQuery] string[] nomCategories)
    {
        var list = _service.GetLivresByFilters(nomAuteur, langue, nomCategories, true);
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    /// <remarks>
    /// Retourne tous les livres recommandés pour un livre donné.
    /// La recommandation retourne des livres du même auteur et des mêmes catégories.
    /// </remarks>
    [Route("/livre/recommandations")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetRecommandation(int issn, string? nomAuteur, [FromQuery] string[] nomCategories)
    {
        var list = _service.GetLivresByFilters(nomAuteur, null, nomCategories, false).ToList();

        list.RemoveAll(l => l.Issn == issn);
        
        
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    /// <remarks>
    /// Crée un livre et retourne son issn
    ///
    ///     POST /livre
    /// 
    ///     {
    ///         "issn": 12345699,
    ///         "titre": "L'Homme qui rit",
    ///         "synopsis": "Ursus et Homo voyagent à travers l’Angleterre en traînant une cahute, dont Ursus se sert pour haranguer les foules et vendre des potions.",
    ///         "dateParution": "1869-04-01",
    ///         "dateAcquisition": "2022-01-27",
    ///         "prixAchat": 10,
    ///         "prixEmprunt": 4,
    ///         "auteurs": [
    ///              {
    ///                  "id": 5,
    ///                  "nom": "Hugo",
    ///                  "prenom": "Victor"
    ///              }
    ///         ],
    ///         "categories": [
    ///              {
    ///                  "nom": "Aventure"
    ///              }
    ///         ]
    ///     }
    ///
    /// </remarks>
    [Route("livre")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateLivre(Livre livre)
    {
        return Created("Created", new { Issn = _service.Insert(livre) });
    }

    /// <remarks>
    /// Modifie un livre et retourne 1 si la modification s'est effectuée
    ///
    ///     PUT /livre
    /// 
    ///     {
    ///         "issn": 12345699,
    ///         "titre": "L'Homme qui rit",
    ///         "synopsis": "Ursus et Homo voyagent à travers l’Angleterre en traînant une cahute, dont Ursus se sert pour haranguer les foules et vendre des potions.",
    ///         "dateParution": "1869-04-01",
    ///         "dateAcquisition": "2022-01-27",
    ///         "prixAchat": 10,
    ///         "prixEmprunt": 7
    ///     }
    ///
    /// </remarks>
    [Route("livre")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateLivre(Livre livre)
    {
        var fetchedLivre = _service.GetLivreByIssn(livre.Issn);
        if (fetchedLivre == null)
            return NotFound();

        return Accepted("Updated", new { AffectedRow = _service.Update(livre) });
    }
    
    /// <remarks>
    /// Supprime un livre et retourne 1 si la suppression s'est effectuée
    /// </remarks>
    [Route("livre")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteLivre(int issn)
    {
        var fetchedLivre = _service.GetLivreByIssn(issn);
        if (fetchedLivre == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(issn) });
    }
}