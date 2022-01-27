using server.Models;

namespace server.Services.Interfaces;

/// <summary>
/// Interface des méthodes du service pour les états d'usure des livres
/// </summary>
public interface IEtatUsureService
{
    IList<EtatUsure> GetEtatsUsure();
    EtatUsure? GetEtatUsureByNom(string nom);
    string? Insert(string nom);
    int Update(string nom, string newNom);
    int Delete(string nom);
}