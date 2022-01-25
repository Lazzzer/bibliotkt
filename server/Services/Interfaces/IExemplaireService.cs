using server.Models;

namespace server.Services.Interfaces;

public interface IExemplaireService
{
    IList<Exemplaire> GetExemplaires();
    List<Exemplaire> GetExemplaireByIssn(int issn);
    int Insert(Exemplaire exemplaire);
    int Update(Exemplaire exemplaire);
    int Delete(int id);
}