using server.Models;

namespace server.Services.Interfaces;

public interface IMembreService
{
    IList<Membre> GetMembres();
    Membre? GetMembreById(int id);
    int Insert(Membre auteur);
    int Update(Membre auteur);
    int Delete(int id);
}