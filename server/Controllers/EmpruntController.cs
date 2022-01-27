using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services.Interfaces;

namespace server.Controllers;

/// <summary>
/// Controller des endpoints traitant des emprunts
/// </summary>
[ApiController]
public class EmpruntController : ControllerBase
{
    private readonly IEmpruntService _service;

    /// <summary>
    /// Constructeur de base
    /// Injection d'un service sur les emprunts
    /// </summary>
    public EmpruntController(IEmpruntService service)
    {
        _service = service;
    }

    /// <remarks>
    /// Retourne tous les emprunts
    /// </remarks>
    [Route("emprunts")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmprunts(bool retard = false)
    {
        var list = retard ? _service.GetEmpruntsEnRetard() : _service.GetEmprunts();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }

    /// <remarks>
    /// Retourne un emprunt par son id
    /// </remarks>
    [Route("emprunt/{id:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmpruntById(int id)
    {
        var emprunt = _service.GetEmpruntById(id);
        if (emprunt == null)
            return NotFound();

        return Ok(emprunt);
    }

    /// <remarks>
    /// Retourne les emprunts d'un membre en pouvant spécifier si l'on souhaite seulement les emprunts en retard
    /// Par défaut, avec le booléen retard à false, tous les emprunts sont récupérés.
    /// </remarks>
    [Route("emprunts/membre/{idMembre:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmpruntsByIdMembre(int idMembre, bool retard = false)
    {
        var list = retard ? _service.GetEmpruntsEnRetardByIdMembre(idMembre) : _service.GetEmpruntsByIdMembre(idMembre);

        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }

    /// <remarks>
    /// Retourne les emprunts d'un exemplaire en pouvant spécifier si l'on souhaite seulement les emprunts en retard
    /// Par défaut, avec le booléen retard à false, tous les emprunts sont récupérés.
    /// </remarks>
    [Route("emprunts/exemplaire/{idExemplaire:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmpruntsByIdExemplaire(int idExemplaire, bool retard = false)
    {
        var list = retard
            ? _service.GetEmpruntsEnRetardByIdExemplaire(idExemplaire)
            : _service.GetEmpruntsByIdExemplaire(idExemplaire);

        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }

    /// <remarks>
    /// Retourne les emprunts d'un livre en pouvant spécifier si l'on souhaite seulement les emprunts en retard
    /// Par défaut, avec le booléen retard à false, tous les emprunts sont récupérés.
    /// </remarks>
    [Route("emprunts/livre/{issn:int}")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmpruntsByIssn(int issn, bool retard = false)
    {
        var list = retard ? _service.GetEmpruntsEnRetardByIssn(issn) : _service.GetEmpruntsByIssn(issn);

        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }

    /// <remarks>
    /// Retourne tous les emprunts actuels en pouvant spécifier si l'on souhaite seulement les emprunts en retard
    /// Par défaut, avec le booléen retard à false, tous les emprunts sont récupérés.
    /// </remarks>
    [Route("emprunts/actuel")]
    [HttpGet]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult GetEmpruntsActuels(bool retard = false)
    {
        var list = retard ? _service.GetEmpruntsActuelsEnRetard() : _service.GetEmpruntsActuels();
        if (list.Count == 0)
            return NotFound();

        return Ok(list);
    }
    
    /// <remarks>
    /// Crée un emprunt et retourne son id
    ///
    ///     POST /emprunt
    /// 
    ///     {
    ///        "dateDebut": "2022-01-27",
    ///        "dateRetourPlanifie": "2022-02-27",
    ///        "etatUsure": "neuf",
    ///        "idExemplaire": 1,
    ///        "idMembre": 1
    ///     }
    ///
    /// </remarks>
    [Route("emprunt")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateEmprunt(Emprunt emprunt)
    {
        return Created("Created", new {Id = _service.Insert(emprunt)});
    }

    /// <remarks>
    /// Modifie un emprunt et retourne 1 si la modification s'est effectuée
    ///
    ///     PUT /emprunt
    /// 
    ///     {
    ///        "id": 16,
    ///        "dateDebut": "2022-01-27",
    ///        "dateRetourPlanifie": "2022-02-27",
    ///        "dateRendu": "2022-02-25",
    ///        "etatUsure": "presque neuf",
    ///        "idExemplaire": 1,
    ///        "idMembre": 1
    ///     }
    ///
    /// </remarks>
    [Route("emprunt")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateEmprunt(Emprunt emprunt)
    {
        var fetchedEmprunt = _service.GetEmpruntById(emprunt.Id);
        if (fetchedEmprunt == null)
            return NotFound();

        return Accepted("Updated", new {AffectedRow = _service.Update(emprunt)});
    }

    /// <remarks>
    /// Supprime un emprunt et retourne 1 si la suppression s'est effectuée
    /// </remarks>
    [Route("emprunt")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteEmprunt(int id)
    {
        var fetchedEmprunt = _service.GetEmpruntById(id);
        if (fetchedEmprunt == null)
            return NotFound();

        return Accepted("Deleted", new {AffectedRow = _service.Delete(id)});
    }
}