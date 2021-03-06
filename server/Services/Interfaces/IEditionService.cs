using server.Models;

namespace server.Services.Interfaces;

/// <summary>
/// Interface des méthodes du service pour les éditions
/// </summary>
public interface IEditionService
{
    IList<Edition> GetEditions();
    IList<Edition> GetEditionsByIssn(int issn);
    Edition? GetEditionById(int id);
    int Insert(Edition edition);
    int Update(Edition edition);
    int Delete(int id);
}