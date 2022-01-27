using server.Models;

namespace server.Services.Interfaces;

/// <summary>
/// Interface des méthodes du service pour les membres
/// </summary>
public interface IMembreService
{
    IList<Membre> GetMembres();
    Membre? GetMembreById(int id);
    int Insert(Membre auteur);
    int Update(Membre auteur);
    int Delete(int id);
}