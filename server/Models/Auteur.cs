namespace server.Models;

public record Auteur
{
  public int Id { get; init; }
  public string Nom { get; init; }
  public string Prenom { get; init; }

  public Auteur(int id, string nom, string prenom)
  {
    Id = id;
    Nom = nom;
    Prenom = prenom;
  }
}