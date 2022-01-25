namespace server.Models;

public record Membre(int Id, string Nom, string Prenom, string Rue, int NoRue, int Npa, string Localite, DateOnly DateCreationCompte, List<Emprunt> Emprunts) 
    : Personne(Id, Nom, Prenom, Rue, NoRue, Npa, Localite, DateCreationCompte);