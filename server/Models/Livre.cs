namespace server.Models;

/// <summary>
/// Record représentant un livre
/// </summary>
public record Livre(int Issn, string Titre, string Synopsis, DateOnly DateParution, DateOnly DateAcquisition,
    int PrixAchat, int PrixEmprunt, List<Auteur>? Auteurs = null, List<Categorie>? Categories = null,
    List<Edition>? Editions = null);