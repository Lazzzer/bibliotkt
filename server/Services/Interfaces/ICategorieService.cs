using server.Models;

namespace server.Services.Interfaces;

/// <summary>
/// Interface des méthodes du service pour les catégories
/// </summary>
public interface ICategorieService
{
    IList<Categorie> GetCategories();
    Categorie? GetCategorieByNom(string nom);
    string? Insert(string nom);
    int Update(string nom, string newNom);
    int Delete(string nom);
}