/**
 * Mets à disposition les méthodes permettant de faire des requêtes sql à la base concernant la table emprunt
 */
using server.Models;

namespace server.Services.Interfaces;
public interface IEmpruntService
{
    IList<Emprunt> GetEmprunts();
    Emprunt? GetEmpruntById(int id);
    IList<Emprunt> GetEmpruntsByIdMembre(int id);
    IList<Emprunt> GetEmpruntsByIdExemplaire(int id);
    IList<Emprunt> GetEmpruntsActuels();
    IList<Emprunt> GetEmpruntsEnRetard();
    IList<Emprunt> GetEmpruntsActuelsEnRetard();
    IList<Emprunt> GetEmpruntsEnRetardByIdMembre(int id);
    IList<Emprunt> GetEmpruntsEnRetardByIdExemplaire(int id);
    int Insert(Emprunt emprunt);
    int Update(Emprunt auteur);
    int Delete(int id);
}