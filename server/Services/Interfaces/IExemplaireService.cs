using server.Models;

namespace server.Services.Interfaces;

/// <summary>
/// Interface des méthodes du service pour les exemplaires
/// </summary>
public interface IExemplaireService
{
    IList<Exemplaire> GetExemplaires();
    Exemplaire? GetExemplaireById(int id);
    List<Exemplaire> GetExemplairesByIssn(int issn);
    int GetNbExemplaires(int issn);
    int Insert(Exemplaire exemplaire);
    int Delete(int id);
}