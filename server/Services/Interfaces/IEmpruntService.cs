using server.Models;

namespace server.Services.Interfaces;

/// <summary>
/// Interface des méthodes du service pour les emprunts
/// </summary>
public interface IEmpruntService
{
    IList<Emprunt> GetEmprunts();
    Emprunt? GetEmpruntById(int id);
    IList<Emprunt> GetEmpruntsByIdMembre(int id);
    IList<Emprunt> GetEmpruntsByIdExemplaire(int id);
    IList<Emprunt> GetEmpruntsByIssn(int id);
    IList<Emprunt> GetEmpruntsActuels();
    IList<Emprunt> GetEmpruntsEnRetard();
    IList<Emprunt> GetEmpruntsActuelsEnRetard();
    IList<Emprunt> GetEmpruntsEnRetardByIdMembre(int id);
    IList<Emprunt> GetEmpruntsEnRetardByIdExemplaire(int id);
    IList<Emprunt> GetEmpruntsEnRetardByIssn(int issn);
    int Insert(Emprunt emprunt);
    int Update(Emprunt emprunt);
    int Delete(int id);
}