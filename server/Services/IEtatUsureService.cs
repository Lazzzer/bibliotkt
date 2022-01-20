using server.Models;

namespace server.Services;

public interface IEtatUsureService
{
    IList<EtatUsure> GetCategories();
    EtatUsure? GetCategorieByNom(string nom);
    string? Insert(string nom);
    int Update(string nom, string newNom);
    int Delete(string nom);
}