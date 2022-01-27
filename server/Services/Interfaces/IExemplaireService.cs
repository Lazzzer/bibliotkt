/**
 * Mets à disposition les méthodes permettant de faire des requêtes sql à la base concernant la table exemplaire
 */
using server.Models;

namespace server.Services.Interfaces;

public interface IExemplaireService
{
    IList<Exemplaire> GetExemplaires();
    Exemplaire? GetExemplaireById(int id);
    List<Exemplaire> GetExemplairesByIssn(int issn);
    int GetNbExemplaires(int issn);
    int Insert(Exemplaire exemplaire);
    int Update(Exemplaire exemplaire);
    int Delete(int id);
}