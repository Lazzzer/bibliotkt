using server.Models;

namespace server.Services;

public interface IAuteurService
{
  IList<Auteur> GetAuteurs();
  Auteur? GetAuteurById(int id);
  Auteur? GetAuteurByIdWithLivres(int id);
  IList<Auteur> GetAuteursByNames(string? nom, string? prenom);
  int Insert(Auteur auteur);
  int Update(Auteur auteur);
  int Delete(int id);
}