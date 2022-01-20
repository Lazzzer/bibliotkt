using server.Models;

namespace server.Services;

public interface IMaisonEditionService
{
    IList<MaisonEdition> GetMaisons();
    MaisonEdition? GetMaisonByNom(string nom);
    MaisonEdition? GetMaisonById(int id);
    int? Insert(MaisonEdition maison);
    int Update(MaisonEdition maison);
    int Delete(int id);
}