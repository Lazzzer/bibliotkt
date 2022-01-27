/**
 * Mets à disposition les méthodes permettant de faire des requêtes sql à la base concernant la table maison d'édition
 */
using server.Models;

namespace server.Services.Interfaces;

public interface IMaisonEditionService
{
    IList<MaisonEdition> GetMaisons();
    MaisonEdition? GetMaisonById(int id);
    int Insert(MaisonEdition maison);
    int Update(MaisonEdition maison);
    int Delete(int id);
}