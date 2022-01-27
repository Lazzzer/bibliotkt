/**
 * Mets à disposition les méthodes permettant de faire des requêtes sql à la base concernant la table édition
 */
using server.Models;

namespace server.Services.Interfaces;

public interface IEditionService
{
    IList<Edition> GetEditions();
    IList<Edition> GetEditionsByIssn(int issn);
    Edition? GetEditionById(int id);
    int Insert(Edition edition);
    int Update(Edition edition);
    int Delete(int id);
}