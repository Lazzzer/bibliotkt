using server.Models;

namespace server.Services.Interfaces;

/// <summary>
/// Interface des méthodes du service pour les maisons d'éditions
/// </summary>
public interface IMaisonEditionService
{
    IList<MaisonEdition> GetMaisons();
    MaisonEdition? GetMaisonById(int id);
    int Insert(MaisonEdition maison);
    int Update(MaisonEdition maison);
    int Delete(int id);
}