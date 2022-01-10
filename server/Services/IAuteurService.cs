using server.Models;

namespace server.Services;

public interface IAuteurService
{
  IList<Auteur> GetAuteurs(int? limit, int? offset);
  Auteur? GetAuteurById(int id);
  IList<Auteur> GetAuteursByNames(string? nom, string? prenom);
  int Insert(Auteur auteur);
  void Update(Auteur auteur);
  void Delete(Auteur auteur);
}