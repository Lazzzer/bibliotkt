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