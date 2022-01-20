using server.Models;

namespace server.Services;

public interface ICategorieService
{
    IList<Categorie> GetCategories();
    Categorie? GetCategorieByNom(string nom);
    string? Insert(string nom);
    int Update(string nom, string newNom);
    int Delete(string nom);
}