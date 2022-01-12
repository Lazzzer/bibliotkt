namespace server.Models;

public abstract record Personne(int Id, string Nom, string Prenom, string Rue, int NoRue, int Npa, int Localite, DateOnly DateCreationCompte);