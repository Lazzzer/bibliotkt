namespace server.Models;

/// <summary>
/// Record représentant un employé héritant du record Personne
/// </summary>
public record Employe(int Id, string Nom, string Prenom, string Rue, int NoRue, int Npa, string Localite,
        DateOnly DateCreationCompte, string Login, string Password)
    : Personne(Id, Nom, Prenom, Rue, NoRue, Npa, Localite, DateCreationCompte);