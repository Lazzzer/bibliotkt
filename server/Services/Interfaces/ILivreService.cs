﻿/**
 * Mets à disposition les méthodes permettant de faire des requêtes sql à la base concernant la table livre
 */
using server.Models;

namespace server.Services.Interfaces;

public interface ILivreService
{
    IList<Livre> GetLivres();
    Livre? GetLivreByIssn(int issn);
    IList<Livre> GetLivresByTitle(string title);
    IList<Livre> GetLivresByFilters(string? nomAuteur, Langue? langue, string[] nomCategories, bool intersect);
    int Insert(Livre livre);
    int Update(Livre livre);
    int Delete(int id);
}