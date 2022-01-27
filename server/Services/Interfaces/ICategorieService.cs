﻿/**
 * Mets à disposition les méthodes permettant de faire des requêtes sql à la base concernant la table catégorie
 */
using server.Models;

namespace server.Services.Interfaces;

public interface ICategorieService
{
    IList<Categorie> GetCategories();
    Categorie? GetCategorieByNom(string nom);
    string? Insert(string nom);
    int Update(string nom, string newNom);
    int Delete(string nom);
}