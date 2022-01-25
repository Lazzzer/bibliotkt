using Microsoft.AspNetCore.Mvc;
using server.Services;

namespace server.Controllers;

[ApiController]
public class CategorieController : ControllerBase
{
    private readonly ICategorieService _service;

    public CategorieController(ICategorieService service)
    {
        _service = service;
    }
    
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
    
    [Route("categorie")]
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult CreateCategorie(string nom)
    {
        var fetchedCategorie = _service.GetCategorieByNom(nom);
        
        if (fetchedCategorie == null)
            return Created("Created", new { Issn = _service.Insert(nom) });

        return BadRequest("Categorie already exists");
    }

    [Route("categorie")]
    [HttpPut]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult UpdateCategorie(string nom, string newNom)
    {
        var fetchedCategorie = _service.GetCategorieByNom(nom);
        if (fetchedCategorie == null)
            return NotFound();
            
        return Accepted("Updated", new { AffectedRow = _service.Update(nom, newNom) });
        
    }
    
    [Route("categorie")]
    [HttpDelete]
    [Produces("application/json")]
    [Consumes("application/json")]
    public ActionResult DeleteCategorie(string nom)
    {
        var fetchedCategorie = _service.GetCategorieByNom(nom);
        if (fetchedCategorie == null)
            return NotFound();

        return Accepted("Deleted", new { AffectedRow = _service.Delete(nom) });
    }
    
}