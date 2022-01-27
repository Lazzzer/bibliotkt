namespace server.Models;

/// <summary>
/// Record représentant une personne
/// </summary>
public abstract record Personne(int Id, string Nom, string Prenom, string Rue, int NoRue, int Npa, string Localite,
    DateOnly DateCreationCompte);