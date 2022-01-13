using server.Models;

namespace server.Services;

public interface ILivreService
{
    IList<Livre> GetLivres();
    Livre? GetLivreByIssn(int issn);
    Livre? GetLivreByIssnWithAuteursEtCategories(int issn);
    IList<Livre> GetLivresByTitle(string? title, string? prenom);
    int Insert(Livre auteur);
    void Update(Livre auteur);
    void Delete(Livre auteur);
}