﻿/**
 * Mets à disposition les méthodes permettant de faire des requêtes sql à la base concernant la table auteur
 */
using server.Models;

namespace server.Services.Interfaces;

public interface IAuteurService
{
  IList<Auteur> GetAuteurs();
  Auteur? GetAuteurById(int id);
  Auteur? GetAuteurByIdWithLivres(int id);
  int Insert(Auteur auteur);
  int Update(Auteur auteur);
  int Delete(int id);
}