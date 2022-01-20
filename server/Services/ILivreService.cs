using server.Models;

namespace server.Services;

public interface ILivreService
{
    IList<Livre> GetLivres();
    Livre? GetLivreByIssn(int issn);
    IList<Livre> GetLivresByTitle(string title);
    IList<Livre> GetLivresByFilters(string? nomAuteur, Langue? langue, string[] nomCategories);
    int Insert(Livre auteur);
    void Update(Livre auteur);
    void Delete(Livre auteur);
}