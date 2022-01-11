namespace server.Models;

public record Auteur(int Id, string Nom, string Prenom, List<Livre>? Livres = null);