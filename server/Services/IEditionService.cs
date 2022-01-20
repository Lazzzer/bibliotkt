using server.Models;

namespace server.Services;

public interface IEditionService
{
    IList<Edition> GetEditions();
    Edition? GetEditionById(int id);
    IList<Edition>? GetEditionByIssn(int issn);
    IList<Edition>? GetEditionByIdMaison(int idMaison);
    IList<Edition>? GetEditionByType(TypeEdition type);
    IList<Edition>? GetEditionByLangue(Langue langue);
    int Insert(Edition edition);
    int Update(Edition edition);
    int Delete(int id);
}