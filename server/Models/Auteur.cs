namespace server.Models;

/// <summary>
/// Record représentant un auteur
/// </summary>
public record Auteur(int Id, string Nom, string Prenom, List<Livre>? Livres = null);