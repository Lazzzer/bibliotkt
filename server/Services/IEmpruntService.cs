using server.Models;

namespace server.Services;
public interface IEmpruntService
{
    IList<Emprunt> GetEmprunts();
    Emprunt? GetEmpruntsById(int id);
    IList<Emprunt> GetEmpruntByIdMembre(int id);
    IList<Emprunt> GetEmpruntByIdExemplaire(int id);
    IList<Emprunt> GetEmpruntActuel();
    IList<Emprunt> GetEmpruntEnRetardByIdMembre(int id);
    int Insert(Emprunt emprunt);
    int Update(Emprunt auteur);
    int Delete(int id);
}