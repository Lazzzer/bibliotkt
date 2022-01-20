using server.Models;

namespace server.Services;

public interface ILivreService
{
    IList<Livre> GetLivres();
    Livre? GetLivreByIssn(int issn);
    Livre? GetLivreByIssnWithAuteursEtCategories(int issn);
    IList<Livre> GetLivresByTitle(string title);
    IList<Livre> GetLivresByFilters(string? nomAuteur, Langue? langue, string[] nomCategories);
    int Insert(Livre livre);
    void Update(Livre livre);
    void Delete(int id);
}