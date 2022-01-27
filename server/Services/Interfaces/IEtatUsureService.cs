/**
 * Mets à disposition les méthodes permettant de faire des requêtes sql à la base concernant la table état d'usure
 */
using server.Models;

namespace server.Services.Interfaces;

public interface IEtatUsureService
{
    IList<EtatUsure> GetEtatsUsure();
    EtatUsure? GetEtatUsureByNom(string nom);
    string? Insert(string nom);
    int Update(string nom, string newNom);
    int Delete(string nom);
}