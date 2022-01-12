using server.Models;

namespace server.Services;

public interface ILivreService
{
    IList<Livre> GetLivres(int? limit, int? offset);
    Livre? GetLivreByIssn(int issn);
    Livre? GetLivreByIssnWithAuteurs(int issn);
    IList<Livre> GetLivresByTitle(string? title, string? prenom);
    int Insert(Livre auteur);
    void Update(Livre auteur);
    void Delete(Livre auteur);
}