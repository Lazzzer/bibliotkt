using server.Models;

namespace server.Services;

public interface IEtatUsureService
{
    IList<EtatUsure> GetCategories();
    EtatUsure? GetCategorieByNom(string nom);
    string? Insert(EtatUsure etat);
    int Update(string nom, string newNom);
    int Delete(EtatUsure etat);
}