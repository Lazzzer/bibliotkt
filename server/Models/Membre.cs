namespace server.Models;

/// <summary>
/// Record représentant un membre héritant du record Personne
/// </summary>
public record Membre(int Id, string Nom, string Prenom, string Rue, int NoRue, int Npa, string Localite,
        DateOnly DateCreationCompte, List<Emprunt>? Emprunts = null)
    : Personne(Id, Nom, Prenom, Rue, NoRue, Npa, Localite, DateCreationCompte);